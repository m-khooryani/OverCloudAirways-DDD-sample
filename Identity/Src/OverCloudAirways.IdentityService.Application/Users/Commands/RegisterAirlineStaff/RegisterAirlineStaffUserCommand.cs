using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.RegisterAirlineStaff;

public record RegisterAirlineStaffUserCommand(
    UserId UserId,
    string Email,
    string GivenName,
    string Surname,
    string Address) : Command;
