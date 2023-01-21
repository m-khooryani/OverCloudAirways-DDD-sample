using Autofac;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace DArch.Infrastructure.ContextAccessors;

public class ContextAccessorModule : Module
{
    private readonly IUserAccessor _accessor;

    public ContextAccessorModule(IUserAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_accessor)
            .AsImplementedInterfaces()
            .As<IUserAccessor>()
            .SingleInstance();
    }
}
