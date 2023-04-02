using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.TestHelpers.Purchases;

public class PurchaseBuilder
{
    private PurchaseId _purchaseId = PurchaseId.New();
    private CustomerId _customerId = CustomerId.New();
    private decimal _amount = 100M;

    public Purchase Build()
    {
        return Purchase.Make(_purchaseId, _customerId, _amount);
    }

    public PurchaseBuilder SetPurchaseId(PurchaseId purchaseId)
    {
        _purchaseId = purchaseId;
        return this;
    }

    public PurchaseBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public PurchaseBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
}
