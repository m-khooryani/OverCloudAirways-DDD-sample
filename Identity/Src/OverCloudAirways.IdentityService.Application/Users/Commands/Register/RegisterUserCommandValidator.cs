using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

internal class RegisterUserCommandValidator : CommandValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(cmd => cmd.GivenName)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(cmd => cmd.UserType)
            .NotEqual(UserType.None)
            .NotEqual(UserType.Customer);
    }
}