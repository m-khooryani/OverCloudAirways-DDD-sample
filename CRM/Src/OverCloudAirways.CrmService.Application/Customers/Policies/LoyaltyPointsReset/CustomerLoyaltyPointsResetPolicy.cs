using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsReset;

public class CustomerLoyaltyPointsResetPolicy : DomainEventPolicy<CustomerLoyaltyPointsResetDomainEvent>
{
    public CustomerLoyaltyPointsResetPolicy(CustomerLoyaltyPointsResetDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
