using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

internal class RegisterUserCommandValidator : CommandValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .MinimumLength(5);
    }
}
