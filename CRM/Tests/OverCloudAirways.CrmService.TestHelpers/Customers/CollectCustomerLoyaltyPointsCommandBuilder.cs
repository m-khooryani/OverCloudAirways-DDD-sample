using OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CollectCustomerLoyaltyPointsCommandBuilder
{
    private CustomerId _customerId = CustomerId.New();
    private decimal _loyaltyPoints = 100_000M;

    public CollectCustomerLoyaltyPointsCommand Build()
    {
        return new CollectCustomerLoyaltyPointsCommand(_customerId, _loyaltyPoints);
    }

    public CollectCustomerLoyaltyPointsCommandBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public CollectCustomerLoyaltyPointsCommandBuilder SetLoyaltyPoints(decimal loyaltyPoints)
    {
        _loyaltyPoints = loyaltyPoints;
        return this;
    }
}
