using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

public class UserRegisteredPolicy : DomainEventPolicy<UserRegisrtredDomainEvent>
{
    public UserRegisteredPolicy(UserRegisrtredDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

internal class UpdateUserReadModelPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisrtredDomainEvent>
{
    public Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        // q publish integration event
        return Task.CompletedTask;
    }
}

internal class PublishUserRegisteredEventPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisrtredDomainEvent>
{
    public Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}