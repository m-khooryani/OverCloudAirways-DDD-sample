using OverCloudAirways.PaymentService.Application.Invoices.Policies.Issued;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceIssuedPolicyBuilder
{
    private InvoiceIssuedDomainEvent _domainEvent = new InvoiceIssuedDomainEventBuilder().Build();

    public InvoiceIssuedPolicy Build()
    {
        return new InvoiceIssuedPolicy(_domainEvent);
    }

    public InvoiceIssuedPolicyBuilder SetDomainEvent(InvoiceIssuedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
