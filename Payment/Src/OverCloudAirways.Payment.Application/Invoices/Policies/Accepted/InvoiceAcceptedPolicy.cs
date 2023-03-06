using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Accepted;

public class InvoiceAcceptedPolicy : DomainEventPolicy<InvoiceAcceptedDomainEvent>
{
    public InvoiceAcceptedPolicy(InvoiceAcceptedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
