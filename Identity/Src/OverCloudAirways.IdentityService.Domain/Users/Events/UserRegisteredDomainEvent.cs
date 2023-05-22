using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.IdentityService.Domain.Users.Events;

public record UserRegisteredDomainEvent(
    UserId UserId,
    UserType UserType,
    string Email,
    string GivenName,
    string Surname) : DomainEvent(UserId);
