using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.Layers;

public class AssemblyLayersModule : Module
{
    private readonly Assembly _domainAssembly;
    private readonly Assembly _applicationAssembly;

    public AssemblyLayersModule(
        Assembly domainAssembly,
        Assembly applicationAssembly)
    {
        _domainAssembly = domainAssembly;
        _applicationAssembly = applicationAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var layers = new AssemblyLayers()
        {
            DomainLayer = _domainAssembly,
            ApplicationLayer = _applicationAssembly,
        };

        builder.RegisterInstance(layers)
            .AsSelf()
            .SingleInstance();
    }
}
