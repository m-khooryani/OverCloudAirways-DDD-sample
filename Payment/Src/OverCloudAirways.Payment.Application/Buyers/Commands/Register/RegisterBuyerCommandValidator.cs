using FluentValidation;
using System.Text.RegularExpressions;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.Register;

internal class RegisterBuyerCommandValidator : CommandValidator<RegisterBuyerCommand>
{
    public RegisterBuyerCommandValidator()
    {
        RuleFor(x => x.BuyerId)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(BeAValidPhoneNumber)
            .WithMessage("Invalid phone number");
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        return Regex.Match(phoneNumber, @"^\d{10}$").Success;
    }
}