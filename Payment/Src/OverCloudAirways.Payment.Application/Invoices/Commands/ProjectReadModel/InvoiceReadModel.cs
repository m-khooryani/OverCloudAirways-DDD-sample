using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;

internal record InvoiceReadModel(
    Guid Id,
    string BuyerFirstName,
    string BuyerLastName,
    DateTimeOffset DueDate,
    decimal TotalAmount,
    IReadOnlyList<InvoiceItem> Items) : ReadModel(Id.ToString(), Id.ToString());