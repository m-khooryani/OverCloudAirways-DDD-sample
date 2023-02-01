using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightSeatsReservedDomainEvent(
    FlightId FlightId,
    int SeatsCount) : DomainEvent(FlightId);
