using System.Text.RegularExpressions;
using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.CrmService.Application.Customers.Commands.Create;

internal class CreateCustomerCommandValidator : CommandValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId)
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

        RuleFor(x => x.DateOfBirth)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(BeAValidPhoneNumber)
            .WithMessage("Invalid phone number");

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        return Regex.Match(phoneNumber, @"^\d{10}$").Success;
    }
}