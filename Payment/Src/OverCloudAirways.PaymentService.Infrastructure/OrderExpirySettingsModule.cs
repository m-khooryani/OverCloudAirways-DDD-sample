using Autofac;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Infrastructure;

public class OrderExpirySettingsModule : Module
{
    private readonly OrderExpirySettings _orderExpirySettings;

    public OrderExpirySettingsModule(OrderExpirySettings orderExpirySettings)
    {
        _orderExpirySettings = orderExpirySettings;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterInstance(_orderExpirySettings)
            .AsSelf()
            .SingleInstance();
    }
}
