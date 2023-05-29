using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

public record RegisterUserCommand(
    UserId UserId,
    UserType UserType,
    string Email,
    string GivenName,
    string Surname,
    string Address) : Command;
