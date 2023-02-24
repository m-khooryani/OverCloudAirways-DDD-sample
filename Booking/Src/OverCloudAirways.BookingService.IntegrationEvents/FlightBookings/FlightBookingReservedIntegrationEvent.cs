using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.IntegrationEvents.FlightBookings;

public record FlightBookingReservedIntegrationEvent(
    FlightBookingId FlightBookingId,
    CustomerId CustomerId,
    FlightId FlightId,
    IReadOnlyList<Passenger> Passengers)
    : IntegrationEvent(FlightId, "booking-flightbooking-reserved");
