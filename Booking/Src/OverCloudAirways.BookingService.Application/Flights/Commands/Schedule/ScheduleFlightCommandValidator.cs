using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.Schedule;

internal class ScheduleFlightCommandValidator : CommandValidator<ScheduleFlightCommand>
{
    public ScheduleFlightCommandValidator()
    {
        RuleFor(x => x.FlightId)
            .NotEmpty();

        RuleFor(x => x.Number)
            .NotEmpty();

        RuleFor(x => x.DepartureAirportId)
            .NotEmpty();

        RuleFor(x => x.DestinationAirportId)
            .NotEmpty();

        RuleFor(x => x.ArrivalTime)
            .GreaterThan(x => x.DepartureTime)
            .WithMessage("Arrival time must be after departure time.");

        RuleFor(x => x.Route)
            .NotEmpty();

        RuleFor(x => x.Distance)
            .GreaterThan(0);

        RuleFor(x => x.AircraftId)
            .NotEmpty();

        RuleFor(x => x.AvailableSeats)
            .GreaterThan(0);

        RuleFor(x => x.BookedSeats)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.MaximumLuggageWeight)
            .GreaterThan(0);
    }
}