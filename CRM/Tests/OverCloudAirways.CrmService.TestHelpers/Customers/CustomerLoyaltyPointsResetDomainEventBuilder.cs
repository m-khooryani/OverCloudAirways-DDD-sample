using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CustomerLoyaltyPointsResetDomainEventBuilder
{
    private CustomerId _customerId = CustomerId.New();

    public CustomerLoyaltyPointsResetDomainEvent Build()
    {
        return new CustomerLoyaltyPointsResetDomainEvent(_customerId);
    }

    public CustomerLoyaltyPointsResetDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }
}
