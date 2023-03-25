using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderCanceledDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();

    public OrderCanceledDomainEvent Build()
    {
        return new OrderCanceledDomainEvent(_orderId);
    }

    public OrderCanceledDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }
}
