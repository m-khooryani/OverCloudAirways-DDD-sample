using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.DiscountApplied;

public class DiscountAppliedToOrderPolicy : DomainEventPolicy<DiscountAppliedToOrderDomainEvent>
{
    public DiscountAppliedToOrderPolicy(DiscountAppliedToOrderDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
