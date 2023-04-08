using OverCloudAirways.PaymentService.Application.Orders.Policies.DiscountApplied;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class DiscountAppliedToOrderPolicyBuilder
{
    private DiscountAppliedToOrderDomainEvent _domainEvent = new DiscountAppliedToOrderDomainEventBuilder().Build();

    public DiscountAppliedToOrderPolicy Build()
    {
        return new DiscountAppliedToOrderPolicy(_domainEvent);
    }

    public DiscountAppliedToOrderPolicyBuilder SetDomainEvent(DiscountAppliedToOrderDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
