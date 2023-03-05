using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Issued;

public class InvoiceIssuedPolicy : DomainEventPolicy<InvoiceIssuedDomainEvent>
{
    public InvoiceIssuedPolicy(InvoiceIssuedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
