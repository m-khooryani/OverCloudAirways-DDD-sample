using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Infrastructure.DomainServices.LoyaltyPrograms;

internal class LoyaltyProgramNameUniqueChecker : ILoyaltyProgramNameUniqueChecker
{
    private readonly ICosmosManager _cosmosManager;

    public LoyaltyProgramNameUniqueChecker(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public async Task<bool> IsUniqueAsync(string name)
    {
        var sql = @$"
                    SELECT c.id FROM c WHERE 
                    c.id = @name AND 
                    c.partitionKey = @name ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@name", name);
        var item = await _cosmosManager.QuerySingleAsync<dynamic>("readmodels", queryDefinition);
        return item is null;
    }
}
