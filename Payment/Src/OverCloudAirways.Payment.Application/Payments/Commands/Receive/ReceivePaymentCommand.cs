using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.Receive;

public record ReceivePaymentCommand(
    PaymentId PaymentId,
    InvoiceId InvoiceId,
    decimal Amount,
    PaymentMethod Method,
    string ReferenceNumber) : Command;
