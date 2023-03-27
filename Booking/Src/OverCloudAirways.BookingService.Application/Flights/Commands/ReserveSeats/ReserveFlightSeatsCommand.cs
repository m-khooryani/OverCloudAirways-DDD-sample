using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;

public record ReserveFlightSeatsCommand(
    FlightId FlightId,
    IReadOnlyList<Passenger> Passengers) : Command;
