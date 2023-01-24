using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

internal class UpdateUserReadModelPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        // q publish integration event command
        return Task.CompletedTask;
    }
}
