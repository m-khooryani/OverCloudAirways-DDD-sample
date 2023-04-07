using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfoById;

internal class GetPromotionInfoByIdQueryHandler : QueryHandler<GetPromotionInfoByIdQuery, PromotionDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetPromotionInfoByIdQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<PromotionDto> HandleAsync(GetPromotionInfoByIdQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    promotion.PromotionId        AS {nameof(PromotionDto.Id)}, 
                    promotion.DiscountCode       AS {nameof(PromotionDto.DiscountCode)}, 
                    promotion.DiscountPercentage AS {nameof(PromotionDto.DiscountPercentage)}, 
                    promotion.Description        AS {nameof(PromotionDto.Description)}, 
                    promotion.ExpirationDate     AS {nameof(PromotionDto.ExpirationDate)}, 
                    promotion.CustomerFirstName  AS {nameof(PromotionDto.CustomerFirstName)}, 
                    promotion.CustomerLastName   AS {nameof(PromotionDto.CustomerLastName)}
                    FROM promotion 
                    WHERE 
                    promotion.PromotionId = @promotionId AND 
                    promotion.partitionKey = 'Promotion'";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@promotionId", query.PromotionId);
        var promotion = await _cosmosManager.QuerySingleAsync<PromotionDto>(ContainersConstants.ReadModels, queryDefinition);

        return promotion;
    }
}