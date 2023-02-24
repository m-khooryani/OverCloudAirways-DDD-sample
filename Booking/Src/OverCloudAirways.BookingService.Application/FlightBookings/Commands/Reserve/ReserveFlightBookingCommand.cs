using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Reserve;

public record ReserveFlightBookingCommand(
    FlightBookingId FlightBookingId,
    CustomerId CustomerId,
    FlightId FlightId,
    IReadOnlyList<Passenger> Passengers) : Command;
