using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Queries.GetInfo;

internal class GetLoyaltyProgramInfoQueryHandler : QueryHandler<GetLoyaltyProgramInfoQuery, LoyaltyProgramDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetLoyaltyProgramInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<LoyaltyProgramDto> HandleAsync(GetLoyaltyProgramInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    loyaltyProgram.LoyaltyProgramId     AS {nameof(LoyaltyProgramDto.Id)}, 
                    loyaltyProgram.Name                 AS {nameof(LoyaltyProgramDto.Name)}, 
                    loyaltyProgram.PurchaseRequirements AS {nameof(LoyaltyProgramDto.PurchaseRequirements)}, 
                    loyaltyProgram.DiscountPercentage   AS {nameof(LoyaltyProgramDto.DiscountPercentage)}
                    FROM loyaltyProgram 
                    WHERE 
                    loyaltyProgram.id = @loyaltyProgramId AND 
                    loyaltyProgram.partitionKey = 'LoyaltyPrograms' ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@loyaltyProgramId", query.LoyaltyProgramId);
        var loyaltyProgram = await _cosmosManager.QuerySingleAsync<LoyaltyProgramDto>(ContainersConstants.ReadModels, queryDefinition);

        return loyaltyProgram;
    }
}