using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;

public record UserReadModel(
    Guid UserId, 
    UserType UserType,
    string Email,
    string GivenName,
    string Surname)
    : ReadModel(UserId.ToString(), UserId.ToString());
