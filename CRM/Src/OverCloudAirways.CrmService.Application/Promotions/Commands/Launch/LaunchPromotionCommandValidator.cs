using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.Launch;

internal class LaunchPromotionCommandValidator : CommandValidator<LaunchPromotionCommand>
{
    public LaunchPromotionCommandValidator()
    {
        RuleFor(cmd => cmd.DiscountPercentage.Value)
            .InclusiveBetween(0, 100);
    }
}