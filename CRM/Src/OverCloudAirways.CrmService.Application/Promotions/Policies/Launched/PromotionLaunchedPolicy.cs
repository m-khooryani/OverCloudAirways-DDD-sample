using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Launched;

public class PromotionLaunchedPolicy : DomainEventPolicy<PromotionLaunchedDomainEvent>
{
    public PromotionLaunchedPolicy(PromotionLaunchedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
