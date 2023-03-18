using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;

internal class CollectCustomerLoyaltyPointsCommandValidator : CommandValidator<CollectCustomerLoyaltyPointsCommand>
{
    public CollectCustomerLoyaltyPointsCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.LoyaltyPoints)
            .GreaterThan(0);
    }
}