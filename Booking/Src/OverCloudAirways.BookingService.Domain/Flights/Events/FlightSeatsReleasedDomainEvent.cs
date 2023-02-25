using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightSeatsReleasedDomainEvent(
    FlightId FlightId,
    int SeatsCount) : DomainEvent(FlightId);
