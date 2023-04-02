using OverCloudAirways.CrmService.Application.Purchases.Commands.Make;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.TestHelpers.Purchases;

public class MakePurchaseCommandBuilder
{
    private PurchaseId _purchaseId = PurchaseId.New();
    private CustomerId _customerId = CustomerId.New();
    private decimal _amount = 100M;

    public MakePurchaseCommand Build()
    {
        return new MakePurchaseCommand(_purchaseId, _customerId, _amount);
    }

    public MakePurchaseCommandBuilder SetPurchaseId(PurchaseId purchaseId)
    {
        _purchaseId = purchaseId;
        return this;
    }

    public MakePurchaseCommandBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public MakePurchaseCommandBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
}
