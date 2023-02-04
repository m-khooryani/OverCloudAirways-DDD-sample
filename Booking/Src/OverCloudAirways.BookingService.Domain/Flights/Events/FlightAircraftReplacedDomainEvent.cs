using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightAircraftReplacedDomainEvent(
    FlightId FlightId,
    AircraftId AircraftId) : DomainEvent(FlightId);
