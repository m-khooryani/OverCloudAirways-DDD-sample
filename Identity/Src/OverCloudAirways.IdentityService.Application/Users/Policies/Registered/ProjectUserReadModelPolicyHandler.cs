using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

internal class ProjectUserReadModelPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectUserReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectUserReadModelCommand(notification.DomainEvent.UserId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
