using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReplaceAircraft;

public record ReplaceFlightAircraftCommand(
    FlightId FlightId,
    AircraftId AircraftId) : Command;
