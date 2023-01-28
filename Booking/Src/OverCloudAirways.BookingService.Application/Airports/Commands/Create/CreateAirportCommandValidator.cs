using FluentValidation;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Airports.Commands.Create;

internal class CreateAirportCommandValidator : CommandValidator<CreateAirportCommand>
{
    public CreateAirportCommandValidator()
    {
        RuleFor(x => x.AirportId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty().MinimumLength(3).MaximumLength(5);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(50);
        RuleForEach(x => x.Terminals).SetValidator(new TerminalValidator());
    }
}

internal class TerminalValidator : AbstractValidator<Terminal>
{
    public TerminalValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Gates)
            .NotEmpty()
            .WithMessage("Terminal must have at least one gate");

        RuleForEach(x => x.Gates)
            .NotEmpty();
    }
}