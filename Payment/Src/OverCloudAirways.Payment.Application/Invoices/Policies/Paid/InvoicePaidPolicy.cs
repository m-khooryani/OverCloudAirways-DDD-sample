using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Paid;

public class InvoicePaidPolicy : DomainEventPolicy<InvoicePaidDomainEvent>
{
    public InvoicePaidPolicy(InvoicePaidDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
