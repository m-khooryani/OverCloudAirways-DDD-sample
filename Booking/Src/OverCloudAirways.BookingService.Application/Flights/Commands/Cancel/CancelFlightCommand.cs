using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.Cancel;

public record CancelFlightCommand(FlightId FlightId) : Command;
