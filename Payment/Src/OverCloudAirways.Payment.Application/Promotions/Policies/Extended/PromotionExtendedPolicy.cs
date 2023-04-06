using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.Application.Promotions.Policies.Extended;

public class PromotionExtendedPolicy : DomainEventPolicy<PromotionExtendedDomainEvent>
{
    public PromotionExtendedPolicy(PromotionExtendedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
