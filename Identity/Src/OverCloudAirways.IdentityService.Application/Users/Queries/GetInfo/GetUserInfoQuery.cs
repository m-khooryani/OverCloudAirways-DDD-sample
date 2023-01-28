using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;

public record GetUserInfoQuery(Guid UserId) : Query<UserDto>;
