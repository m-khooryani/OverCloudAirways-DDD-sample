using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Events;

public record FlightBookingConfirmedDomainEvent(FlightBookingId FlightBookingId) : DomainEvent(FlightBookingId);
