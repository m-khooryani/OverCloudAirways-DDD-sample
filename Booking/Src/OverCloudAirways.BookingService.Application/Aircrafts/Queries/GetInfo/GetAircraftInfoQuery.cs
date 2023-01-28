using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Queries.GetInfo;

public record GetAircraftInfoQuery(Guid AircraftId) : Query<AircraftReadDto>;
