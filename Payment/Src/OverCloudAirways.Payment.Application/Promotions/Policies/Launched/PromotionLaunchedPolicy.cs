using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.Application.Promotions.Policies.Launched;

public class PromotionLaunchedPolicy : DomainEventPolicy<PromotionLaunchedDomainEvent>
{
    public PromotionLaunchedPolicy(PromotionLaunchedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
