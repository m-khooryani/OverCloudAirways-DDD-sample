using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Plan;

internal class PlanLoyaltyProgramCommandValidator : CommandValidator<PlanLoyaltyProgramCommand>
{
    public PlanLoyaltyProgramCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PurchaseRequirements)
            .GreaterThan(0);

        RuleFor(x => x.DiscountPercentage.Value)
            .InclusiveBetween(0, 100);
    }
}