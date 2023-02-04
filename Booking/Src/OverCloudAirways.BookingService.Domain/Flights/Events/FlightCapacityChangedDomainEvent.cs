using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightCapacityChangedDomainEvent(
    FlightId FlightId,
    int Capacity) : DomainEvent(FlightId);
