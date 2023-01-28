using FluentValidation;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Commands.Create;

internal class CreateAircraftCommandValidator : CommandValidator<CreateAircraftCommand>
{
    public CreateAircraftCommandValidator()
    {
        RuleFor(x => x.AircraftId).NotEmpty();
        RuleFor(x => x.Type).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Manufacturer).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(255);
        RuleFor(x => x.SeatingCapacity).GreaterThan(0);
        RuleFor(x => x.EconomyCostPerKM).GreaterThan(0);
        RuleFor(x => x.FirstClassCostPerKM).GreaterThan(0);
        RuleFor(x => x.Range).GreaterThan(0);
        RuleFor(x => x.CruisingAltitude).GreaterThan(0);
        RuleFor(x => x.MaxTakeoffWeight).GreaterThan(0);
        RuleFor(x => x.Length).GreaterThan(0);
        RuleFor(x => x.Wingspan).GreaterThan(0);
        RuleFor(x => x.Height).GreaterThan(0);
        RuleFor(x => x.Engines).NotEmpty();
        RuleForEach(x => x.Engines).SetValidator(new EngineValidator());
    }
}

internal class EngineValidator : AbstractValidator<Engine>
{
    public EngineValidator()
    {
        RuleFor(x => x.Type).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Thrust).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Diameter).NotEmpty().GreaterThan(0);
        RuleFor(x => x.DryWeight).NotEmpty().GreaterThan(0);
    }
}
