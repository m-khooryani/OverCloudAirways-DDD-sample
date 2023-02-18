using OverCloudAirways.PaymentService.Application.Buyers.Policies.Registered;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Buyers;

public class BuyerRegisteredPolicyBuilder
{
    private BuyerRegisteredDomainEvent _domainEvent = new BuyerRegisteredDomainEventBuilder().Build();

    public BuyerRegisteredPolicy Build()
    {
        return new BuyerRegisteredPolicy(_domainEvent);
    }

    public BuyerRegisteredPolicyBuilder SetDomainEvent(BuyerRegisteredDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}