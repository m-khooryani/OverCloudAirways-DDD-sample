using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;

internal class RefundBuyerBalanceCommandValidator : CommandValidator<RefundBuyerBalanceCommand>
{
    public RefundBuyerBalanceCommandValidator()
    {
        RuleFor(x => x.BuyerId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Refund amount must be greater than 0.");
    }
}
