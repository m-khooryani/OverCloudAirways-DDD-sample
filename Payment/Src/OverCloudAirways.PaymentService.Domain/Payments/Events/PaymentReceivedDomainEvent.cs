using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Domain.Payments.Events;

public record PaymentReceivedDomainEvent(
    PaymentId PaymentId,
    decimal Amount,
    InvoiceId InvoiceId,
    PaymentMethod Method,
    string ReferenceNumber) : DomainEvent(PaymentId);
