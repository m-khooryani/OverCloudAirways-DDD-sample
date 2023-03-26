using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Confirm;

public record ConfirmOrderCommand(
    OrderId OrderId,
    InvoiceId InvoiceId) : Command;
