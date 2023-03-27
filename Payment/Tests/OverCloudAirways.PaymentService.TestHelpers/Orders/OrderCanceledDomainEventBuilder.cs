using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderCanceledDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();
    private BuyerId _buyerId = BuyerId.New();
    private decimal _totalAmount = 100M;

    public OrderCanceledDomainEvent Build()
    {
        return new OrderCanceledDomainEvent(_orderId, _buyerId, _totalAmount);
    }

    public OrderCanceledDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public OrderCanceledDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public OrderCanceledDomainEventBuilder SetTotalAmount(decimal totalAmount)
    {
        _totalAmount = totalAmount;
        return this;
    }
}
