using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Extended;

internal class EnqueueProjectingReadModelPromotionExtendedPolicyHandler : IDomainPolicyHandler<PromotionExtendedPolicy, PromotionExtendedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelPromotionExtendedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionExtendedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPromotionReadModelCommand(notification.DomainEvent.PromotionId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}