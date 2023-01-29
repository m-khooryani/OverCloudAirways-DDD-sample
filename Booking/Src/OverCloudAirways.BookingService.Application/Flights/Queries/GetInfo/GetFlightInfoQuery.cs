using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BookingService.Application.Flights.Queries.GetInfo;

public record GetFlightInfoQuery(Guid FlightId) : Query<FlightDto>;
