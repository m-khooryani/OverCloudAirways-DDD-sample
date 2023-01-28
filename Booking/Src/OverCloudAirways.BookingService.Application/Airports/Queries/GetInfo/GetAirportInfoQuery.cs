using OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BookingService.Application.Airports.Queries.GetInfo;

public record GetAirportInfoQuery(string AirportCode) : Query<AirportDto>;
