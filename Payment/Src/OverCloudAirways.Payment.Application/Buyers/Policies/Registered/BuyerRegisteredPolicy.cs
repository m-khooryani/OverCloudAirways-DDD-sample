using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;

namespace OverCloudAirways.PaymentService.Application.Buyers.Policies.Registered;

public class BuyerRegisteredPolicy : DomainEventPolicy<BuyerRegisteredDomainEvent>
{
    public BuyerRegisteredPolicy(BuyerRegisteredDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
