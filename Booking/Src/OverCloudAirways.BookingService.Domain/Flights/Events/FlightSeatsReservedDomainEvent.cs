using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Flights.Events;

public record FlightSeatsReservedDomainEvent(
    FlightId FlightId,
    CustomerId CustomerId,
    IReadOnlyList<Passenger> Passengers) : DomainEvent(FlightId);
