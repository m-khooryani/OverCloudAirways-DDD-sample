using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;

internal record ProjectInvoiceReadModelCommand(InvoiceId InvoiceId) : Command;
