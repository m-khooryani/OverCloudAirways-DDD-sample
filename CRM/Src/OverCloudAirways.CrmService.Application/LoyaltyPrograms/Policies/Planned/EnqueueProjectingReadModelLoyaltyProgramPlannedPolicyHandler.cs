using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Policies.Planned;

internal class EnqueueProjectingReadModelLoyaltyProgramPlannedPolicyHandler : IDomainPolicyHandler<LoyaltyProgramPlannedPolicy, LoyaltyProgramPlannedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelLoyaltyProgramPlannedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(LoyaltyProgramPlannedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectLoyaltyProgramReadModelCommand(notification.DomainEvent.LoyaltyProgramId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}