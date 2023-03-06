using OverCloudAirways.PaymentService.Application.Invoices.Policies.Accepted;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceAcceptedPolicyBuilder
{
    private InvoiceAcceptedDomainEvent _domainEvent = new InvoiceAcceptedDomainEventBuilder().Build();

    public InvoiceAcceptedPolicy Build()
    {
        return new InvoiceAcceptedPolicy(_domainEvent);
    }

    public InvoiceAcceptedPolicyBuilder SetDomainEvent(InvoiceAcceptedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}