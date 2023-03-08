using OverCloudAirways.PaymentService.Application.Payments.Policies.Received;
using OverCloudAirways.PaymentService.Domain.Payments.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Payments;

public class PaymentReceivedPolicyBuilder
{
    private PaymentReceivedDomainEvent _domainEvent = new PaymentReceivedDomainEventBuilder().Build();

    public PaymentReceivedPolicy Build()
    {
        return new PaymentReceivedPolicy(_domainEvent);
    }

    public PaymentReceivedPolicyBuilder SetDomainEvent(PaymentReceivedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
