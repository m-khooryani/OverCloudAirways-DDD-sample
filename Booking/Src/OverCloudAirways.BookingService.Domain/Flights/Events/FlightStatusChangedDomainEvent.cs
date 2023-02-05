using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightStatusChangedDomainEvent(
    FlightId FlightId,
    FlightStatus Status) : DomainEvent(FlightId);
