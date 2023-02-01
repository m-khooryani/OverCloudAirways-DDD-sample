using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;

internal class ReserveFlightSeatsCommandValidator : CommandValidator<ReserveFlightSeatsCommand>
{
    public ReserveFlightSeatsCommandValidator()
    {
        RuleFor(cmd => cmd.FlightId)
            .NotEmpty();

        RuleFor(cmd => cmd.SeatsCount)
            .GreaterThan(0);
    }
}