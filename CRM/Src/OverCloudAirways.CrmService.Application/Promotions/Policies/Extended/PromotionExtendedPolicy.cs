using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Extended;

public class PromotionExtendedPolicy : DomainEventPolicy<PromotionExtendedDomainEvent>
{
    public PromotionExtendedPolicy(PromotionExtendedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
