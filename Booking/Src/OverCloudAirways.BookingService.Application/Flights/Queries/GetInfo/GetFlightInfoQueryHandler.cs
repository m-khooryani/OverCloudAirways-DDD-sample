using Microsoft.Azure.Cosmos;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;

namespace OverCloudAirways.BookingService.Application.Flights.Queries.GetInfo;

internal class GetFlightInfoQueryHandler : QueryHandler<GetFlightInfoQuery, FlightDto>
{
    private readonly ICosmosManager _cosmosManager;

    public GetFlightInfoQueryHandler(ICosmosManager cosmosManager)
    {
        _cosmosManager = cosmosManager;
    }

    public override async Task<FlightDto> HandleAsync(GetFlightInfoQuery query, CancellationToken cancellationToken)
    {
        var sql = @$"
                    SELECT 
                    flight.FlightId             AS {nameof(FlightDto.Id)}, 
                    flight.Number               AS {nameof(FlightDto.Number)}, 
                    flight.DepartureAirport     AS {nameof(FlightDto.DepartureAirport)}, 
                    flight.DestinationAirport   AS {nameof(FlightDto.DestinationAirport)}, 
                    flight.DepartureTime        AS {nameof(FlightDto.DepartureTime)},
                    flight.ArrivalTime          AS {nameof(FlightDto.ArrivalTime)},
                    flight.Route                AS {nameof(FlightDto.Route)},
                    flight.Distance             AS {nameof(FlightDto.Distance)},
                    flight.AircraftModel        AS {nameof(FlightDto.AircraftModel)},
                    flight.AvailableSeats       AS {nameof(FlightDto.AvailableSeats)},
                    flight.BookedSeats          AS {nameof(FlightDto.BookedSeats)},
                    flight.MaximumLuggageWeight AS {nameof(FlightDto.MaximumLuggageWeight)},
                    flight.EconomyPrice         AS {nameof(FlightDto.EconomyPrice)},
                    flight.FirstClassPrice      AS {nameof(FlightDto.FirstClassPrice)}
                    FROM flight 
                    WHERE 
                    flight.id = @flightId AND 
                    flight.partitionKey = @flightId ";

        var queryDefinition = new QueryDefinition(sql)
            .WithParameter("@flightId", query.FlightId);
        var flight = await _cosmosManager.QuerySingleAsync<FlightDto>(ContainersConstants.ReadModels, queryDefinition);

        return flight;
    }
}
