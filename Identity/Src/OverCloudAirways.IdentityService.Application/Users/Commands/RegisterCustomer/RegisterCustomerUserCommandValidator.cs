using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

internal class RegisterCustomerUserCommandValidator : CommandValidator<RegisterCustomerUserCommand>
{
    public RegisterCustomerUserCommandValidator()
    {
        RuleFor(cmd => cmd.GivenName)
            .NotEmpty()
            .MinimumLength(5);
    }
}
