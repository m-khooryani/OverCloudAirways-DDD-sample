using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.CustomerRegistered;

internal class PublishUserRegisteredEventPolicyHandler : IDomainPolicyHandler<CustomerUserRegisteredPolicy, CustomerUserRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishUserRegisteredEventPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public Task Handle(CustomerUserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
        //var @event = new UserRegisteredIntegrationEvent(
        //    notification.DomainEvent.UserId,
        //    notification.DomainEvent.Name);

        //await _commandsScheduler.EnqueueAsync(new PublishIntegrationEventCommand(@event));
    }
}