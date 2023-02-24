using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Events;

public record FlightBookingReservedDomainEvent(
    FlightBookingId FlightBookingId,
    CustomerId CustomerId,
    FlightId FlightId,
    IReadOnlyList<Passenger> Passengers) : DomainEvent(FlightBookingId);
