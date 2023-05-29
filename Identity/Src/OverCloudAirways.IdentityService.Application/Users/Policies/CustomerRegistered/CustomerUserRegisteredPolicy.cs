using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.CustomerRegistered;

public class CustomerUserRegisteredPolicy : DomainEventPolicy<CustomerUserRegisteredDomainEvent>
{
    public CustomerUserRegisteredPolicy(CustomerUserRegisteredDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
