using FluentValidation;

namespace OverCloudAirways.IdentityService.Application.Users.Register;

internal class RegisterUserCommandValidator : CommandValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .MinimumLength(5);
    }
}
