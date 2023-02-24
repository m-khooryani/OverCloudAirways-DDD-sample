using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Events;

public record FlightBookingCancelledDomainEvent(FlightBookingId FlightBookingId) : DomainEvent(FlightBookingId);
