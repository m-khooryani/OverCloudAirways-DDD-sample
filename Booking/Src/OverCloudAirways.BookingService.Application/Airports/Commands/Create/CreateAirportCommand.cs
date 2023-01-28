using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.Create;

public record CreateAirportCommand(
    AirportId AirportId,
    string Code,
    string Name,
    string Location,
    IReadOnlyList<Terminal> Terminals) : Command;
