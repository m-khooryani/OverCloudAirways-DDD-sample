using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderFailedDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();
    private BuyerId _buyerId = BuyerId.New();
    private decimal _paidAmount = 100M;

    public OrderFailedDomainEvent Build()
    {
        return new OrderFailedDomainEvent(_orderId, _buyerId, _paidAmount);
    }

    public OrderFailedDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public OrderFailedDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public OrderFailedDomainEventBuilder SetPaidAmount(decimal paidAmount)
    {
        _paidAmount = paidAmount;
        return this;
    }
}
