using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightCanceledDomainEvent(FlightId FlightId) : DomainEvent(FlightId);
