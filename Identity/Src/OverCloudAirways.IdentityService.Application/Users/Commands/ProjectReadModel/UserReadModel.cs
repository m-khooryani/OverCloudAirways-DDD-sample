using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.UpdateReadModel;

public record UserReadModel(Guid Id, string Name)
    : ReadModel(Id.ToString(), Id.ToString());
