using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.IdentityService.Domain.Users.Events;

public record CustomerUserRegisteredDomainEvent(
    UserId UserId,
    string Email,
    string GivenName,
    string Surname,
    string Address) : DomainEvent(UserId);
