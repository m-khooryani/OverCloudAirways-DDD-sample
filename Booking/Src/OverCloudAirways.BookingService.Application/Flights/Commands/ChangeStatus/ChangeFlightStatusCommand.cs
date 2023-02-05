using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeStatus;

public record ChangeFlightStatusCommand(
    FlightId FlightId,
    FlightStatus Status) : Command;
