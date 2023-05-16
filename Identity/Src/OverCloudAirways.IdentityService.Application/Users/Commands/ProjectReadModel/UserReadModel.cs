using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;

public record UserReadModel(Guid UserId, string GivenName)
    : ReadModel(UserId.ToString(), UserId.ToString());
