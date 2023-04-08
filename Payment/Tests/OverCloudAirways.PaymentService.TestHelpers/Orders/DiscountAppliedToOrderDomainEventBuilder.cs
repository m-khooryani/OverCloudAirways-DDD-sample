using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class DiscountAppliedToOrderDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();
    private Percentage _appliedDiscount = Percentage.Of(20M);

    public DiscountAppliedToOrderDomainEvent Build()
    {
        return new DiscountAppliedToOrderDomainEvent(_orderId, _appliedDiscount);
    }

    public DiscountAppliedToOrderDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public DiscountAppliedToOrderDomainEventBuilder SetAppliedDiscount(Percentage appliedDiscount)
    {
        _appliedDiscount = appliedDiscount;
        return this;
    }
}
