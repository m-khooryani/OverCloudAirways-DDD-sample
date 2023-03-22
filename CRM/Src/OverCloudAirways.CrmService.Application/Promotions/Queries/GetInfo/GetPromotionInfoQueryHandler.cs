using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfo;

internal class GetPromotionInfoQueryHandler : QueryHandler<GetPromotionInfoQuery, PromotionDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetPromotionInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<PromotionDto> HandleAsync(GetPromotionInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    promotion.Id                 AS {nameof(PromotionDto.Id)}, 
                    promotion.DiscountCode       AS {nameof(PromotionDto.DiscountCode)}, 
                    promotion.DiscountPercentage AS {nameof(PromotionDto.DiscountPercentage)}, 
                    promotion.Description        AS {nameof(PromotionDto.Description)}, 
                    promotion.ExpirationDate     AS {nameof(PromotionDto.ExpirationDate)}, 
                    promotion.CustomerFirstName  AS {nameof(PromotionDto.CustomerFirstName)}, 
                    promotion.CustomerLastName   AS {nameof(PromotionDto.CustomerLastName)}
                    FROM promotion 
                    WHERE 
                    promotion.id = @discountCode AND 
                    promotion.partitionKey = @discountCode ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@discountCode", query.DiscountCode);
        var promotion = await _cosmosManager.QuerySingleAsync<PromotionDto>(ContainersConstants.ReadModels, queryDefinition);

        return promotion;
    }
}