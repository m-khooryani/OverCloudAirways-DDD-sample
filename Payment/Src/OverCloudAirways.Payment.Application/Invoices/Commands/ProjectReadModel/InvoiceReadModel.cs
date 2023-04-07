using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;

internal record InvoiceReadModel(
    Guid InvoiceId,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset DueDate,
    decimal TotalAmount,
    IReadOnlyList<InvoiceItem> Items) : ReadModel(InvoiceId.ToString(), InvoiceId.ToString());