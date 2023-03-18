using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CustomerLoyaltyPointsCollectedDomainEventBuilder
{
    private CustomerId _customerId = CustomerId.New();
    private decimal _loyaltyPoints = 100M;

    public CustomerLoyaltyPointsCollectedDomainEvent Build()
    {
        return new CustomerLoyaltyPointsCollectedDomainEvent(_customerId, _loyaltyPoints);
    }

    public CustomerLoyaltyPointsCollectedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public CustomerLoyaltyPointsCollectedDomainEventBuilder SetLoyaltyPoints(decimal loyaltyPoints)
    {
        _loyaltyPoints = loyaltyPoints;
        return this;
    }
}
