using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.Extend;

internal class ExtendPromotionCommandValidator : CommandValidator<ExtendPromotionCommand>
{
    public ExtendPromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId)
            .NotNull();

        RuleFor(x => x.Months)
            .GreaterThan(0)
            .WithMessage("The number of months to extend the promotion must be greater than 0.");
    }
}