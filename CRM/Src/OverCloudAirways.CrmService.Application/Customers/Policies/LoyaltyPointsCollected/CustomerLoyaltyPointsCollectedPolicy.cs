using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsCollected;

public class CustomerLoyaltyPointsCollectedPolicy : DomainEventPolicy<CustomerLoyaltyPointsCollectedDomainEvent>
{
    public CustomerLoyaltyPointsCollectedPolicy(CustomerLoyaltyPointsCollectedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
