using DArch.Application.Configuration.Commands;
using OverCloudAirways.BuildingBlocks.Application.Commands.PublishIntegrationEvent;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.IdentityService.Domain.Users.Events;
using OverCloudAirways.IdentityService.IntegrationEvents.Users;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

internal class PublishUserRegisteredEventPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishUserRegisteredEventPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new UserRegisteredIntegrationEvent(
            notification.DomainEvent.UserId,
            notification.DomainEvent.Name);

        await _commandsScheduler.EnqueueAsync(new PublishIntegrationEventCommand(@event));
    }
}