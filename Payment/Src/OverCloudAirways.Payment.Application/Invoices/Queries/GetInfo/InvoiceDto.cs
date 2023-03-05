using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Queries.GetInfo;

public record InvoiceDto(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset DueDate,
    decimal TotalAmount,
    IReadOnlyList<InvoiceItem> Items);
