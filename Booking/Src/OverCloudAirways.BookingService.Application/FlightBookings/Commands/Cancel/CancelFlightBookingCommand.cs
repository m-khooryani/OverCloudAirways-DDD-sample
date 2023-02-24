using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Cancel;

public record CancelFlightBookingCommand(FlightBookingId FlightBookingId) : Command;
