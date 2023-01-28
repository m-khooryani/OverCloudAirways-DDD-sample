using Microsoft.Azure.Cosmos;
using OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Airports.Queries.GetInfo;

internal class GetAirportInfoQueryHandler : QueryHandler<GetAirportInfoQuery, AirportDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetAirportInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<AirportDto> HandleAsync(GetAirportInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    airport.Id        AS {nameof(AirportDto.Id)}, 
                    airport.Code      AS {nameof(AirportDto.Code)}, 
                    airport.Name      AS {nameof(AirportDto.Name)}, 
                    airport.Location  AS {nameof(AirportDto.Location)}, 
                    airport.Terminals AS {nameof(AirportDto.Terminals)}
                    FROM airport 
                    WHERE 
                    airport.id = @airportCode AND 
                    airport.partitionKey = @airportCode ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@airportCode", query.AirportCode);
        var airport = await _cosmosManager.QuerySingleAsync<AirportDto>(ContainersConstants.ReadModels, queryDefinition);

        return airport;
    }
}
