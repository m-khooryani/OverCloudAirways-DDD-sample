using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;

public record ProjectFlightReadModelCommand(FlightId FlightId) : Command;
