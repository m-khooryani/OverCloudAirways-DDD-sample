using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChargePrice;

internal record ChargeFlightPriceCommand(FlightId FlightId) : Command;
