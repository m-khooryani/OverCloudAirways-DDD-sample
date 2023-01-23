using Autofac;
using DArch.AzureServiceBus;
using DArch.Infrastructure.EventBus;
using DArch.ServiceBus;

namespace DArch.Infrastructure.Configuration;

public class AzureServiceBusModule : Module
{
    private readonly ServiceBusConfig _config;
    private IServiceBusSenderFactory? _serviceBusSenderFactory;

    public AzureServiceBusModule(
        ServiceBusConfig config,
        IServiceBusSenderFactory? serviceBusSenderFactory = null)
    {
        _config = config;
        _serviceBusSenderFactory = serviceBusSenderFactory;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterInstance(_config)
            .AsSelf()
            .As<ServiceBusConfig>()
            .SingleInstance();

        if (_serviceBusSenderFactory is not null)
        {
            builder
                .RegisterInstance(_serviceBusSenderFactory)
                .As<IServiceBusSenderFactory>()
                .SingleInstance();
        }
        else
        {
            builder
                .RegisterType(typeof(ServiceBusSenderFactory))
                .As<IServiceBusSenderFactory>()
                .SingleInstance();
        }

        builder.RegisterType(typeof(AzureServiceBusEventBus))
            .As(typeof(IEventBus))
            .SingleInstance();
    }
}