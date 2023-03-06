using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Accept;

public record AcceptInvoiceCommand(InvoiceId InvoiceId) : Command;
