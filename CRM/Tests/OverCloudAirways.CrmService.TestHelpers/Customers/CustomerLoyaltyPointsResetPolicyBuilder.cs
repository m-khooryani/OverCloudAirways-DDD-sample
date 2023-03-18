using OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsReset;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CustomerLoyaltyPointsResetPolicyBuilder
{
    private CustomerLoyaltyPointsResetDomainEvent _domainEvent = new CustomerLoyaltyPointsResetDomainEventBuilder().Build();

    public CustomerLoyaltyPointsResetPolicy Build()
    {
        return new CustomerLoyaltyPointsResetPolicy(_domainEvent);
    }

    public CustomerLoyaltyPointsResetPolicyBuilder SetDomainEvent(CustomerLoyaltyPointsResetDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}