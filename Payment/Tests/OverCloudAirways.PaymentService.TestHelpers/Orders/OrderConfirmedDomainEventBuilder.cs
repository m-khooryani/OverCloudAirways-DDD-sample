using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderConfirmedDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();

    public OrderConfirmedDomainEvent Build()
    {
        return new OrderConfirmedDomainEvent(_orderId);
    }

    public OrderConfirmedDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }
}
