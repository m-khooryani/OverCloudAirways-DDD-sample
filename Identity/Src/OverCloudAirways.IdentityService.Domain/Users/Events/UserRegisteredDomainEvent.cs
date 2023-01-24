using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.IdentityService.Domain.Users.Events;

public record UserRegisteredDomainEvent(
    UserId UserId,
    string Name) : DomainEvent(UserId);
