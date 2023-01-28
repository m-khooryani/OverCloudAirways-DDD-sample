using System.Reflection;
using Autofac;
using MediatR;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.CommandPipelines;
using OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.PolicyPipelines;
using OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.QueryPipelines;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing;

public class RequestProcessingModule : Autofac.Module
{
    private readonly Assembly _applicationAssembly;

    public RequestProcessingModule(Assembly applicationAssembly)
    {
        _applicationAssembly = applicationAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CommandsScheduler>()
            .As<ICommandsScheduler>()
            .InstancePerLifetimeScope();

        // Command Pipelines
        builder.RegisterGeneric(typeof(CommandLoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(CommandValidationBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(CommandUnitOfWorkBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));

        // Query Pipelines
        builder.RegisterGeneric(typeof(QueryLoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(QueryValidationBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));

        // Policy Decorators
        builder.RegisterGenericDecorator(
            typeof(PolicyLoggingDecorator<>),
            typeof(INotificationHandler<>));

        builder.RegisterAssemblyTypes(_applicationAssembly)
            .AsClosedTypesOf(typeof(DomainEventPolicy<>))
            .InstancePerDependency();
    }
}
