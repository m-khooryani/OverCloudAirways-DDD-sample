using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

public class UserRegisteredPolicy : DomainEventPolicy<UserRegisteredDomainEvent>
{
    public UserRegisteredPolicy(UserRegisteredDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
