using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.IdentityService.Domain.Users.Events;

public record UserRegisrtredDomainEvent(
    UserId UserId,
    string Name) : DomainEvent(UserId);
