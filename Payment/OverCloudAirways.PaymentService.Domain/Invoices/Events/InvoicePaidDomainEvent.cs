using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Invoices.Events;

public record InvoicePaidDomainEvent(InvoiceId InvoiceId) : DomainEvent(InvoiceId);
