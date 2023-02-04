using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeCapacity;

public record ChangeFlightCapacityCommand(
    FlightId FlightId,
    int Capacity) : Command;
