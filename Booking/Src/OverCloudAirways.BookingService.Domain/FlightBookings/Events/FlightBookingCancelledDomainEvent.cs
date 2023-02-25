using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Events;

public record FlightBookingCancelledDomainEvent(
    FlightBookingId FlightBookingId,
    FlightId FlightId,
    int SeatsCount) : DomainEvent(FlightBookingId);
