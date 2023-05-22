using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.Register;

public record RegisterCustomerUserCommand(
    UserId UserId,
    string Email,
    string GivenName,
    string Surname) : Command;
