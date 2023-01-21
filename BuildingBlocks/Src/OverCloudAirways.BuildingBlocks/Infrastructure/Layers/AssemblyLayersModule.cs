using System.Reflection;
using Autofac;
using DArch.Infrastructure.CleanArchitecture;
using Module = Autofac.Module;

namespace DArch.Infrastructure.Configuration.Processing.ApplicationAssembly;

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
