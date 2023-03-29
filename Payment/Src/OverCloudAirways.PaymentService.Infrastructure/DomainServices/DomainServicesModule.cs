using Autofac;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Infrastructure.DomainServices.Products;

namespace OverCloudAirways.PaymentService.Infrastructure.DomainServices;

public class DomainServicesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<ConnectedOrders>()
            .As<IConnectedOrders>()
            .SingleInstance();
    }
}
