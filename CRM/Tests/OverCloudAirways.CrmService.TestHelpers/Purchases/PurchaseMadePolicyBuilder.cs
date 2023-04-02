using OverCloudAirways.CrmService.Application.Purchases.Policies.Made;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Purchases;

public class PurchaseMadePolicyBuilder
{
    private PurchaseMadeDomainEvent _domainEvent = new PurchaseMadeDomainEventBuilder().Build();

	public PurchaseMadePolicy Build()
    {
        return new PurchaseMadePolicy(_domainEvent);
    }

    public PurchaseMadePolicyBuilder SetDomainEvent(PurchaseMadeDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}