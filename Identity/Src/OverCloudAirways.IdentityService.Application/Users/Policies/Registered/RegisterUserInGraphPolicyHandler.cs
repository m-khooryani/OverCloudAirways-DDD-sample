using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.IdentityService.Application.Users.Commands.RegisterInGraph;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Application.Users.Policies.Registered;

internal class RegisterUserInGraphPolicyHandler : IDomainPolicyHandler<UserRegisteredPolicy, UserRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public RegisterUserInGraphPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(UserRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        var command = new RegisterUserInGraphCommand(notification.DomainEvent.UserId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
