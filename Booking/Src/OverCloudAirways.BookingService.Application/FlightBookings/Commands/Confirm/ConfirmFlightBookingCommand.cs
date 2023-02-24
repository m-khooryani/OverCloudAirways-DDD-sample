using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Confirm;

public record ConfirmFlightBookingCommand(FlightBookingId FlightBookingId) : Command;
