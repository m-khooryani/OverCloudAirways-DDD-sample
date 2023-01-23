using DArch.Application.Contracts;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

public record RegisterUserCommand(
    UserId UserId,
    string Name) : Command;
