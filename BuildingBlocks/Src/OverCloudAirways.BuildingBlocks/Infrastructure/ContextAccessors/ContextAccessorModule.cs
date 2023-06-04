using Autofac;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.ContextAccessors;

public class ContextAccessorModule : Module
{
    private readonly IUserAccessor _accessor;

    public ContextAccessorModule(IUserAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType(_accessor.GetType())
            .AsImplementedInterfaces()
            .As<IUserAccessor>()
            .InstancePerLifetimeScope();
    }
}
