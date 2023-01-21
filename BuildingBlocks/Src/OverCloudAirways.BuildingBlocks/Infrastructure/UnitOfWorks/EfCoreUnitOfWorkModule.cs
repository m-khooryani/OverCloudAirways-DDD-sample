using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks;
using OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

namespace DArch.UnitOfWorks.EFCore;

public class EfCoreUnitOfWorkModule<T> : Autofac.Module 
    where T : BuildingBlocksDbContext
{
    private readonly DbContextOptionsBuilder<T> _dbContextOptions;
    private readonly Assembly _assembly;

    public EfCoreUnitOfWorkModule(DbContextOptionsBuilder<T> dbContextOptions,
        Assembly infrastructureAssembly)
    {
        _dbContextOptions = dbContextOptions;
        _assembly = infrastructureAssembly;
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

        builder.RegisterAssemblyTypes(_assembly)
            .Where(type => type.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<AggregateRepository>()
            .As<IAggregateRepository>()
            .InstancePerLifetimeScope();

        // UnitOfWork
        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();
        builder.RegisterDecorator(
            typeof(AppendingAggregateHistoryUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(PublishOutboxMessagesUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(CreateOutboxMessagesUnitOfWorkDecorator),
            typeof(IUnitOfWork));
        builder.RegisterDecorator(
            typeof(LoggingUnitOfWorkDecorator),
            typeof(IUnitOfWork));
    }
}
