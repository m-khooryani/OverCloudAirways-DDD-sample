using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.IntegrationEvents.Users;

public record UserRegisteredIntegrationEvent(
    UserId UserId,
    string Name) : IntegrationEvent(UserId, "identity-user-registered");
