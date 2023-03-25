using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderExpiredDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();

    public OrderExpiredDomainEvent Build()
    {
        return new OrderExpiredDomainEvent(_orderId);
    }

    public OrderExpiredDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }
}
