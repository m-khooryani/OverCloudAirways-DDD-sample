using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.PaymentService.Application.Invoices.Queries.GetInfo;

public record GetInvoiceInfoQuery(Guid InvoiceId) : Query<InvoiceDto>;
