using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Queries.GetInfo;

internal class GetAircraftInfoQueryHandler : QueryHandler<GetAircraftInfoQuery, AircraftReadDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetAircraftInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<AircraftReadDto> HandleAsync(GetAircraftInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    aircraft.id                  AS {nameof(AircraftReadDto.Id)}, 
                    aircraft.Type                AS {nameof(AircraftReadDto.Type)}, 
                    aircraft.Manufacturer        AS {nameof(AircraftReadDto.Manufacturer)}, 
                    aircraft.Model               AS {nameof(AircraftReadDto.Model)}, 
                    aircraft.SeatingCapacity     AS {nameof(AircraftReadDto.SeatingCapacity)}, 
                    aircraft.EconomyCostPerKM    AS {nameof(AircraftReadDto.EconomyCostPerKM)}, 
                    aircraft.FirstClassCostPerKM AS {nameof(AircraftReadDto.FirstClassCostPerKM)}, 
                    aircraft.Range               AS {nameof(AircraftReadDto.Range)}, 
                    aircraft.CruisingAltitude    AS {nameof(AircraftReadDto.CruisingAltitude)}, 
                    aircraft.MaxTakeoffWeight    AS {nameof(AircraftReadDto.MaxTakeoffWeight)}, 
                    aircraft.Length              AS {nameof(AircraftReadDto.Length)}, 
                    aircraft.Wingspan            AS {nameof(AircraftReadDto.Wingspan)}, 
                    aircraft.Height              AS {nameof(AircraftReadDto.Height)}, 
                    aircraft.Engines             AS {nameof(AircraftReadDto.Engines)}
                    FROM aircraft 
                    WHERE 
                    aircraft.id = @aircraftId AND 
                    aircraft.partitionKey = @aircraftId ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@aircraftId", query.AircraftId);
        var aircraft = await _cosmosManager.QuerySingleAsync<AircraftReadDto>(ContainersConstants.ReadModels, queryDefinition);

        return aircraft;
    }
}
