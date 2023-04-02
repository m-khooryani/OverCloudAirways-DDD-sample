using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.Domain.Purchases;

public class Purchase : AggregateRoot<PurchaseId>
{
    public CustomerId CustomerId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public decimal Amount { get; private set; }

    private Purchase()
    {
    }

    public static Purchase Make(
        PurchaseId purchaseId,
        CustomerId customerId,
        decimal amount)
    {
        var @event = new PurchaseMadeDomainEvent(purchaseId, customerId, Clock.Now, amount);

        var purchase = new Purchase();
        purchase.Apply(@event);

        return purchase;
    }

    protected void When(PurchaseMadeDomainEvent @event)
    {
        Id = @event.PurchaseId;
        CustomerId = @event.CustomerId;
        Date = @event.Date;
        Amount = @event.Amount;
    }
}
