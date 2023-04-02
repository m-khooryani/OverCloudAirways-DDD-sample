using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.Application.Purchases.Policies.Made;

public class PurchaseMadePolicy : DomainEventPolicy<PurchaseMadeDomainEvent>
{
    public PurchaseMadePolicy(PurchaseMadeDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
