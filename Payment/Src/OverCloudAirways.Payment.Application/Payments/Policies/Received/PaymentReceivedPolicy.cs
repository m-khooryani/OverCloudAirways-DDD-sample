using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Payments.Events;

namespace OverCloudAirways.PaymentService.Application.Payments.Policies.Received;

public class PaymentReceivedPolicy : DomainEventPolicy<PaymentReceivedDomainEvent>
{
    public PaymentReceivedPolicy(PaymentReceivedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
