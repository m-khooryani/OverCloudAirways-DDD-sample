using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.BuildingBlocks.Infrastructure.AzureServiceBus;
using OverCloudAirways.BuildingBlocks.Infrastructure.ContextAccessors;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.BuildingBlocks.Infrastructure.Json;
using OverCloudAirways.BuildingBlocks.Infrastructure.Layers;
using OverCloudAirways.BuildingBlocks.Infrastructure.Logging;
using OverCloudAirways.BuildingBlocks.Infrastructure.Mediation;
using OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing;
using OverCloudAirways.BuildingBlocks.Infrastructure.RetryPolicy;
using OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

namespace OverCloudAirways.IdentityService.API.GraphIntegration;

internal static class ApplicationModules
{
    public static IServiceCollection RegisterApplicationComponents(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<CqrsInvoker>();

        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

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

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<BuildingBlocksDbContext>();

        var accountEndpoint = configuration["DatabaseUrl"];
        var accountKey = configuration["PrimaryKey"];
        const string DATABASE_ID = "IdentityDB";

        dbContextOptionsBuilder.UseCosmos(accountEndpoint!, accountKey!, DATABASE_ID);

        var unitOfWorkModule = new EfCoreUnitOfWorkModule<BuildingBlocksDbContext>(dbContextOptionsBuilder, infrastructureAssembly);
        var loggingModule = new LoggingModule(loggerFactory, "identity");
        var contextAccessorModule = new ContextAccessorModule(new B2CUserAccessor());
        var retryPolicyModule = new RetryPolicyModule(new PollyConfig()
        {
            SleepDurations = Array.Empty<TimeSpan>()
        });
        var serviceBusConfig = new ServiceBusConfig()
        {
            ConnectionString = configuration["ServiceBusConnectionString"],
            OutboxQueueName = configuration["ServiceBusOutboxQueueName"]
        };

        var azureServiceBusModule = new AzureServiceBusModule(serviceBusConfig);

        var jsonSettings = new JsonSerializerSettings()
        {
            Converters =
            {
                new EnumerationJsonConverter(),
                new TypedIdJsonConverter()
            },
            ContractResolver = new ValueObjectsConstructorResolver()
        };

        var jsonModule = new NewtonsoftJsonModule(jsonSettings);
        var cosmosDbModule = new CosmosNewtonsoftIntegrationModule(accountEndpoint!, accountKey!, DATABASE_ID, jsonSettings);

        CompositionRoot.Initialize(
            assemblyLayersModule,
            processingModule,
            mediatorModule,
            unitOfWorkModule,
            loggingModule,
            contextAccessorModule,
            retryPolicyModule,
            azureServiceBusModule,
            jsonModule,
            cosmosDbModule);

        return services;
    }
}
