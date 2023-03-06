using OverCloudAirways.PaymentService.Application.Invoices.Policies.Paid;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoicePaidPolicyBuilder
{
    private InvoicePaidDomainEvent _domainEvent = new InvoicePaidDomainEventBuilder().Build();

    public InvoicePaidPolicy Build()
    {
        return new InvoicePaidPolicy(_domainEvent);
    }

    public InvoicePaidPolicyBuilder SetDomainEvent(InvoicePaidDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
