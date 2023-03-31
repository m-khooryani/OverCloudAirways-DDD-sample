using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Domain.Orders.Events;

public record OrderConfirmedDomainEvent(
    OrderId OrderId,
    InvoiceId InvoiceId) : DomainEvent(OrderId);
