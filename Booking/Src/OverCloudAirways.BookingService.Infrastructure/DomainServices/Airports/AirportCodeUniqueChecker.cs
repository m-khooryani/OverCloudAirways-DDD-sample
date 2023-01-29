using Microsoft.Azure.Cosmos;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Infrastructure.DomainServices.Airports;

internal class AirportCodeUniqueChecker : IAirportCodeUniqueChecker
{
    private readonly ICosmosManager _cosmosManager;

    public AirportCodeUniqueChecker(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public async Task<bool> IsUniqueAsync(string code)
    {
        var sql = @$"
                    SELECT c.id FROM c WHERE 
                    c.id = @code AND 
                    c.partitionKey = @code ";
        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@code", code);
        var item = await _cosmosManager.QuerySingleAsync<dynamic>("readmodels", queryDefinition);
        return item is null;
    }
}
