using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.PaymentService.Application.Invoices.Queries.GetInfo;

internal class GetInvoiceInfoQueryHandler : QueryHandler<GetInvoiceInfoQuery, InvoiceDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetInvoiceInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<InvoiceDto> HandleAsync(GetInvoiceInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    invoice.Id             AS {nameof(InvoiceDto.Id)}, 
                    invoice.BuyerFirstName AS {nameof(InvoiceDto.BuyerFirstName)}, 
                    invoice.BuyerLastName  AS {nameof(InvoiceDto.BuyerLastName)}, 
                    invoice.DueDate        AS {nameof(InvoiceDto.DueDate)}, 
                    invoice.TotalAmount    AS {nameof(InvoiceDto.TotalAmount)},
                    invoice.Items          AS {nameof(InvoiceDto.Items)}
                    FROM invoice 
                    WHERE 
                    invoice.id = @invoiceId AND 
                    invoice.partitionKey = @invoiceId 
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@invoiceId", query.InvoiceId);
        var order = await _cosmosManager.QuerySingleAsync<InvoiceDto>(ContainersConstants.ReadModels, queryDefinition);

        return order;
    }
}
