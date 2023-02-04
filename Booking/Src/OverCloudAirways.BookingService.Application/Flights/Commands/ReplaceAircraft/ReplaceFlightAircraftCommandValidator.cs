using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReplaceAircraft;

internal class ReplaceFlightAircraftCommandValidator : CommandValidator<ReplaceFlightAircraftCommand>
{
    public ReplaceFlightAircraftCommandValidator()
    {
        RuleFor(cmd => cmd.FlightId)
            .NotNull()
            .NotEmpty();

        RuleFor(cmd => cmd.AircraftId)
            .NotNull()
            .NotEmpty();
    }
}