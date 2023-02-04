using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeCapacity;

internal class ChangeFlightCapacityCommandValidator : CommandValidator<ChangeFlightCapacityCommand>
{
    public ChangeFlightCapacityCommandValidator()
    {
        RuleFor(cmd => cmd.Capacity)
            .GreaterThan(0);
    }
}