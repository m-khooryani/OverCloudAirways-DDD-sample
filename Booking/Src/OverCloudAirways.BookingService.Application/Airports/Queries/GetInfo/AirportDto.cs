using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.ProjectReadModel;

public record AirportDto(
    Guid Id,
    string Code,
    string Name,
    string Location,
    IReadOnlyCollection<Terminal> Terminals);
