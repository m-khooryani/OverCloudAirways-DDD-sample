using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.Purchases.Commands.Make;

internal class MakePurchaseCommandValidator : CommandValidator<MakePurchaseCommand>
{
    public MakePurchaseCommandValidator()
    {
        RuleFor(x => x.PurchaseId)
            .NotNull();

        RuleFor(x => x.CustomerId)
            .NotNull();

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Amount must be a non-negative value.");
    }
}