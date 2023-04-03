using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Infrastructure.DomainServices.LoyaltyPrograms;

internal class ActiveLoyaltyPrograms : IActiveLoyaltyPrograms
{
    private readonly ICosmosManager _cosmosManager;

    public ActiveLoyaltyPrograms(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public async Task<ReadOnlyCollection<LoyaltyProgramId>> GetLoyaltyProgramIds()
    {
        var sql = @$"
                    SELECT 
                    loyaltyPrograms.Id AS {nameof(LoyaltyProgramModel.Id)}
                    FROM loyaltyPrograms
                    WHERE 
                    loyaltyPrograms.partitionKey = 'LoyaltyPrograms' AND
                    NOT loyaltyPrograms.IsSuspended
        ";
        var queryDefinition = new QueryDefinition(sql);
        var loyaltyPrograms = await _cosmosManager.AsListAsync<LoyaltyProgramModel>("readmodels", queryDefinition);

        return loyaltyPrograms
            .Where(x => x.Id is not null)
            .Select(x => x.Id)
            .ToList()
            .AsReadOnly();
    }

    private class LoyaltyProgramModel
    {
        public LoyaltyProgramId Id { get; set; }
    }
}
