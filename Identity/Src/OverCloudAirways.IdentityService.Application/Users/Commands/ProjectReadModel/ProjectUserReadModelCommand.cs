using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;

public record ProjectUserReadModelCommand(UserId UserId) : Command;
