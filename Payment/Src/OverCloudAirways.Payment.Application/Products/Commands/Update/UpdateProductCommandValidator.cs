using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.Update;

internal class UpdateProductCommandValidator : CommandValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
