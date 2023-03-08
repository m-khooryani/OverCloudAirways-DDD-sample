using System.Collections.ObjectModel;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Domain.Invoices.Events;

public record InvoiceIssuedDomainEvent(
    InvoiceId InvoiceId,
    BuyerId BuyerId,
    DateTimeOffset DueDate,
    ReadOnlyCollection<InvoiceItem> InvoiceItems) : DomainEvent(InvoiceId);
