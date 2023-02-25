using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReleaseSeats;

internal class ReleaseFlightSeatsCommandValidator : CommandValidator<ReleaseFlightSeatsCommand>
{
    public ReleaseFlightSeatsCommandValidator()
    {
        RuleFor(cmd => cmd.FlightId)
            .NotEmpty();

        RuleFor(cmd => cmd.SeatsCount)
            .GreaterThan(0);
    }
}