using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Place;

internal class PlaceOrderCommandValidator : CommandValidator<PlaceOrderCommand>
{
    public PlaceOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
        RuleFor(x => x.BuyerId)
            .NotEmpty();
        RuleFor(x => x.OrderItems)
            .NotEmpty();
        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemValidator());
    }

    class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
