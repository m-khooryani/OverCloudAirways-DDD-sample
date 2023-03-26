using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderFailedDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();

    public OrderFailedDomainEvent Build()
    {
        return new OrderFailedDomainEvent(_orderId);
    }

    public OrderFailedDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }
}
