using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Layers;

public class AssemblyLayersModule : Module
{
    private readonly Assembly _domainAssembly;

    public AssemblyLayersModule(Assembly domainAssembly)
    {
        _domainAssembly = domainAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var layers = new AssemblyLayers()
        {
            DomainLayer = _domainAssembly
        };

        builder.RegisterInstance(layers)
            .AsSelf()
            .SingleInstance();
    }
}
