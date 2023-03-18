using OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsCollected;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CustomerLoyaltyPointsCollectedPolicyBuilder
{
    private CustomerLoyaltyPointsCollectedDomainEvent _domainEvent = new CustomerLoyaltyPointsCollectedDomainEventBuilder().Build();

    public CustomerLoyaltyPointsCollectedPolicy Build()
    {
        return new CustomerLoyaltyPointsCollectedPolicy(_domainEvent);
    }

    public CustomerLoyaltyPointsCollectedPolicyBuilder SetDomainEvent(CustomerLoyaltyPointsCollectedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}