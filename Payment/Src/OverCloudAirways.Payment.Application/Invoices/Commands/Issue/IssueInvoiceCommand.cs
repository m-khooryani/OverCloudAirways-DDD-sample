using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Issue;

public record IssueInvoiceCommand(
    InvoiceId InvoiceId,
    OrderId OrderId) : Command;
