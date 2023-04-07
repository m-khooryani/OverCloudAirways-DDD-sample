using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.CrmService.Application.Promotions.Queries.GetList;

internal class GetPromotionListQueryHandler : QueryHandler<GetPromotionListQuery, PagedDto<PromotionDto>>
{
    private readonly ICosmosManager _cosmosManager;

    public GetPromotionListQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<PagedDto<PromotionDto>> HandleAsync(GetPromotionListQuery query, CancellationToken cancellationToken)
    {
        var fromWhere = $@"
                    FROM promotion 
                    WHERE 
                    promotion.partitionKey = 'Promotion'";

        var sql = @$"
                    SELECT 
                    promotion.PromotionId        AS {nameof(PromotionDto.Id)}, 
                    promotion.DiscountCode       AS {nameof(PromotionDto.DiscountCode)}, 
                    promotion.DiscountPercentage AS {nameof(PromotionDto.DiscountPercentage)}, 
                    promotion.Description        AS {nameof(PromotionDto.Description)}, 
                    promotion.ExpirationDate     AS {nameof(PromotionDto.ExpirationDate)}, 
                    promotion.CustomerFirstName  AS {nameof(PromotionDto.CustomerFirstName)}, 
                    promotion.CustomerLastName   AS {nameof(PromotionDto.CustomerLastName)}
                    {fromWhere}";

        var queryDefinition = new QueryDefinition(sql);
        var promotions = await _cosmosManager.AsPagedAsync(
            ContainersConstants.ReadModels, 
            query,
            queryDefinition,
            fromWhere);

        return promotions;
    }
}
