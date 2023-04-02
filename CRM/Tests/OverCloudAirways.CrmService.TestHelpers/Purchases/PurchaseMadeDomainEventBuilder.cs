using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Purchases;

public class PurchaseMadeDomainEventBuilder
{
    private PurchaseId _purchaseId = PurchaseId.New();
    private CustomerId _customerId = CustomerId.New();
    private DateTimeOffset _date = Clock.Now;
    private decimal _amount = 100M;

    public PurchaseMadeDomainEvent Build()
    {
        return new PurchaseMadeDomainEvent(_purchaseId, _customerId, _date, _amount);
    }

    public PurchaseMadeDomainEventBuilder SetPurchaseId(PurchaseId purchaseId)
    {
        _purchaseId = purchaseId;
        return this;
    }

    public PurchaseMadeDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public PurchaseMadeDomainEventBuilder SetDate(DateTimeOffset date)
    {
        _date = date;
        return this;
    }

    public PurchaseMadeDomainEventBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
}
