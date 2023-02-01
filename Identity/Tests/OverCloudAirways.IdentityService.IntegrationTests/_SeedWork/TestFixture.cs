using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OverCloudAirways.BuildingBlocks.Application.Commands.ProcessOutboxMessage;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;
using OverCloudAirways.BuildingBlocks.Infrastructure.ContextAccessors;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.BuildingBlocks.Infrastructure.Layers;
using OverCloudAirways.BuildingBlocks.Infrastructure.Logging;
using OverCloudAirways.BuildingBlocks.Infrastructure.Mediation;
using OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing;
using OverCloudAirways.BuildingBlocks.Infrastructure.RetryPolicy;
using OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;
using OverCloudAirways.IdentityService.Application;
using Xunit.Abstractions;

namespace OverCloudAirways.IdentityService.IntegrationTests._SeedWork;

public class TestFixture : IDisposable
{
    public static ITestOutputHelper Output { get; set; }
    public static CqrsInvoker Invoker { get; private set; }

    private readonly string _databaseId = "IdentityDBTest_" + Guid.NewGuid().ToString()[..6];

    private static readonly Action<BuildingBlocksDbContext> _clearDbAction = context =>
    {
        var properties = context
            .GetType()
            .GetProperties()
            .Where(property => property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
        foreach (var property in properties)
        {
            var dbSet = context.GetType().GetProperty(property.Name).GetValue(context, null) as dynamic;
            DbSetUtility.Clear(dbSet);
        }
    };

    public TestFixture()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", true)
            .AddEnvironmentVariables()
            .Build();

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<BuildingBlocksDbContext>();

        var accountEndpoint = configuration["cosmosdburi"];
        var accountKey = configuration["primarykey"];

        dbContextOptionsBuilder.UseCosmos(accountEndpoint!, accountKey!, _databaseId);
        dbContextOptionsBuilder.UseLoggerFactory(GetLoggerFactory());
        SetupCompositionRoot(dbContextOptionsBuilder, accountEndpoint, accountKey);

        Invoker = new CqrsInvoker();
        InitialDatabase();
    }

    private void SetupCompositionRoot(
        DbContextOptionsBuilder<BuildingBlocksDbContext> contextOptionsBuilder,
        string? accountEndpoint,
        string? accountKey)
    {
        var domainAssembly = Assembly.Load("OverCloudAirways.IdentityService.Domain");
        var infrastructureAssembly = Assembly.Load("OverCloudAirways.IdentityService.Infrastructure");
        var applicationAssembly = Assembly.Load("OverCloudAirways.IdentityService.Application");

        var assemblyLayersModule = new AssemblyLayersModule(domainAssembly);
        var processingModule = new RequestProcessingModule(applicationAssembly);
        var mediatorModule = new MediatorModule(
            new[]
            {
                applicationAssembly
            },
            new[]
            {
                typeof(IRequestHandler<>),
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
            });
        var unitOfWorkModule = new EfCoreUnitOfWorkModule<BuildingBlocksDbContext>(contextOptionsBuilder, infrastructureAssembly);
        var loggingModule = new LoggingModule(GetLoggerFactory(), "integrationTest");
        var contextAccessorModule = new ContextAccessorModule(FakeAccessor.Instance);
        var retryPolicyModule = new RetryPolicyModule(new PollyConfig()
        {
            SleepDurations = Array.Empty<TimeSpan>()
        });
        var serviceBusConfig = Substitute.For<ServiceBusConfig>();
        var azureServiceBusModule = new AzureServiceBusModule(serviceBusConfig);

        var cosmosDbModule = new CosmosDBModule(accountEndpoint!, accountKey!, _databaseId);

        CompositionRoot.Initialize(
            assemblyLayersModule,
            processingModule,
            mediatorModule,
            unitOfWorkModule,
            loggingModule,
            contextAccessorModule,
            retryPolicyModule,
            azureServiceBusModule,
            cosmosDbModule);
    }

    private static LoggerFactory GetLoggerFactory()
    {
        return new LoggerFactory(new[]
        {
            new LogToActionLoggerProvider(
                new Dictionary<string, LogLevel>()
                {
                    { "Default", LogLevel.Debug },
                    { "Microsoft", LogLevel.Trace },
                },
                log =>
                {
                    try
                    {
                        Output?.WriteLine(log);
                    }
                    catch
                    {
                    }
                }
            )
        });
    }

    void IDisposable.Dispose()
    {
        using var scope = CompositionRoot.BeginLifetimeScope();

        var database = scope.Resolve<Database>();
        database.DeleteAsync().Wait();
    }

    internal static TService ResolveService<TService>()
        where TService : notnull
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var service = scope.Resolve<TService>();
        return service;
    }

    private void InitialDatabase()
    {
        using var scope = CompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<BuildingBlocksDbContext>();
        context.Database.EnsureCreated();

        var database = scope.Resolve<Database>();
        _ = database.CreateContainerIfNotExistsAsync(ContainersConstants.User, "/partitionKey").Result;
    }

    internal async Task ResetAsync()
    {
        await ClearDatabase();
        FakeAccessor.Reset();
        Clock.Reset();
    }

    private async Task ClearDatabase()
    {
        await using var scope = CompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<BuildingBlocksDbContext>();

        _clearDbAction(context);

        await context.SaveChangesAsync();
    }

    internal async Task ProcessLastOutboxMessageAsync()
    {
        await using var scope = CompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<BuildingBlocksDbContext>();
        var message = await context.OutboxMessages.OrderBy(x => x.OccurredOn).LastAsync();

        await Invoker.CommandAsync(new ProcessOutboxCommand(message.Id.ToString()));

        // Check for failing. exceptions are handled by retry policy
        message = await context.OutboxMessages.Where(x => x.Id == message.Id).FirstOrDefaultAsync();
        if (message is not null)
        {
            await context.Entry(message).ReloadAsync();
            throw new OutboxMessageProccessingFailedException($"messageId: {message.Id}, Exception: {message.Error}");
        }
    }
}
