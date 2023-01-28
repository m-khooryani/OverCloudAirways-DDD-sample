using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Airports.Events;

public record AirportCreatedDomainEvent(
    AirportId AirportId,
    string Code,
    string Name,
    string Location,
    IReadOnlyList<Terminal> Terminals) : DomainEvent(AirportId);
