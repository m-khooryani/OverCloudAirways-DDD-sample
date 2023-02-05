using FluentValidation;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeStatus;

internal class ChangeFlightStatusCommandValidator : CommandValidator<ChangeFlightStatusCommand>
{
    public ChangeFlightStatusCommandValidator()
    {
        RuleFor(cmd=>cmd.Status)
            .NotEqual(FlightStatus.None)
            .NotEqual(FlightStatus.Cancelled)
            .WithMessage("The status cannot be set to Canceled using the ChangeFlightStatusCommand. Please use the CancelFlightCommand to cancel the flight.");
    }
}