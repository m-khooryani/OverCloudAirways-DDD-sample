using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Pay;

public record PayInvoiceCommand(InvoiceId InvoiceId) : Command;
