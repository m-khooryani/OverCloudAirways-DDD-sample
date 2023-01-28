using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;

internal record AirportReadModel(
    Guid Id,
    string Code,
    string Name,
    string Location,
    IReadOnlyCollection<Terminal> Terminals) : ReadModel(Code, Code);
