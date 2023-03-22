using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Launched;

internal class EnqueueProjectingReadModelPromotionLaunchedPolicyHandler : IDomainPolicyHandler<PromotionLaunchedPolicy, PromotionLaunchedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelPromotionLaunchedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionLaunchedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPromotionReadModelCommand(notification.DomainEvent.PromotionId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
