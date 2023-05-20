using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;

public class EfCoreUnitOfWorkModule<T> : Autofac.Module
    where T : BuildingBlocksDbContext
{
    private readonly DbContextOptionsBuilder<T> _dbContextOptions;
    private readonly Assembly _infrastructureAssembly;

    public EfCoreUnitOfWorkModule(DbContextOptionsBuilder<T> dbContextOptions,
        Assembly infrastructureAssembly)
    {
        _dbContextOptions = dbContextOptions;
        _infrastructureAssembly = infrastructureAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(c =>
            {
                return (T)Activator.CreateInstance(typeof(T), _dbContextOptions.Options);
            })
            .AsSelf()
            .As<BuildingBlocksDbContext>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<AggregateRepository>()
            .As<IAggregateRepository>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<OutboxRepository>()
            .As<IOutboxRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(_infrastructureAssembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // UnitOfWork
        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();
        builder.RegisterDecorator(
            typeof(AppendingAggregateHistoryUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(CreateOutboxMessagesUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(PublishOutboxMessagesUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(LoggingUnitOfWorkDecorator),
            typeof(IUnitOfWork));
    }
}
