using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReleaseSeats;

public record ReleaseFlightSeatsCommand(
    FlightId FlightId,
    int SeatsCount) : Command;
