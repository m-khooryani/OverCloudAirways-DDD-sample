using Autofac;

namespace DArch.Infrastructure.Configuration;

public static class CompositionRoot
{
    private static IContainer _container;

    public static void Initialize(params Module[] modules)
    {
        var containerBuilder = new ContainerBuilder();

        foreach (var module in modules)
        {
            containerBuilder.RegisterModule(module);
        }

        _container = containerBuilder.Build();
    }

    public static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
