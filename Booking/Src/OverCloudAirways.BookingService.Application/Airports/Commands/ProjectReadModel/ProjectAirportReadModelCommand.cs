using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;

public record ProjectAirportReadModelCommand(AirportId AirportId) : Command;
