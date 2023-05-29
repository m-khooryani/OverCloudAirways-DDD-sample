using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Application.Users.Commands.ProjectReadModel;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.CustomerRegistered;

internal class ProjectUserReadModelPolicyHandler : IDomainPolicyHandler<CustomerUserRegisteredPolicy, CustomerUserRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectUserReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(CustomerUserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectUserReadModelCommand(notification.DomainEvent.UserId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
