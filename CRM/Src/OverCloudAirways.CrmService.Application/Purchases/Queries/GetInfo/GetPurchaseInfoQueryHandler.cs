using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.CrmService.Application.Purchases.Queries.GetInfo;

internal class GetPurchaseInfoQueryHandler : QueryHandler<GetPurchaseInfoQuery, PurchaseDto>
{
    private ICosmosManager _cosmosManager;

    public GetPurchaseInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<PurchaseDto> HandleAsync(GetPurchaseInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    purchase.PurchaseId        AS {nameof(PurchaseDto.PurchaseId)}, 
                    purchase.CustomerId        AS {nameof(PurchaseDto.CustomerId)}, 
                    purchase.CustomerFirstName AS {nameof(PurchaseDto.CustomerFirstName)}, 
                    purchase.CustomerLastName  AS {nameof(PurchaseDto.CustomerLastName)}, 
                    purchase.Date              AS {nameof(PurchaseDto.Date)}, 
                    purchase.Amount            AS {nameof(PurchaseDto.Amount)}
                    FROM purchase 
                    WHERE 
                    purchase.id = @purchaseId AND
                    purchase.partitionKey = @customerId
        ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@purchaseId", query.PurchaseId)
            .WithParameter("@customerId", query.CustomerId);
        var purchase = await _cosmosManager.QuerySingleAsync<PurchaseDto>(ContainersConstants.ReadModels, queryDefinition);

        return purchase;
    }
}