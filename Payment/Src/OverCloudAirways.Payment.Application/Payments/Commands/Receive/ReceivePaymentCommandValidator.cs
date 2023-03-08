using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.Receive;

internal class ReceivePaymentCommandValidator : CommandValidator<ReceivePaymentCommand>
{
    public ReceivePaymentCommandValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty();

        RuleFor(x => x.InvoiceId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.Method)
            .NotEqual(PaymentMethod.None);

        RuleFor(x => x.ReferenceNumber)
            .NotEmpty()
            .MaximumLength(50);
    }
}
