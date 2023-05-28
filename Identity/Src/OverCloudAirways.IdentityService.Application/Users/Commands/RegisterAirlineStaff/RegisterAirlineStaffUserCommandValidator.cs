using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.RegisterAirlineStaff;

internal class RegisterAirlineStaffUserCommandValidator : CommandValidator<RegisterAirlineStaffUserCommand>
{
    public RegisterAirlineStaffUserCommandValidator()
    {
        RuleFor(cmd => cmd.GivenName)
            .NotEmpty()
            .MinimumLength(3);
    }
}