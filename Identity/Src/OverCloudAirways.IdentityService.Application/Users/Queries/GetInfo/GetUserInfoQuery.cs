using DArch.Application.Contracts;

namespace OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;

public record GetUserInfoQuery(Guid UserId) : Query<UserDto>;
