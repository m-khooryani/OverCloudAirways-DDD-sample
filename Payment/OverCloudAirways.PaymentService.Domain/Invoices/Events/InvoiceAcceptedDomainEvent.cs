using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.Invoices.Events;

public record InvoiceAcceptedDomainEvent(InvoiceId InvoiceId) : DomainEvent(InvoiceId);
