using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Promotions.Events;
using OverCloudAirways.CrmService.IntegrationEvents.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Extended;

internal class PublishIntegrationEventPolicyHandler : IDomainPolicyHandler<PromotionExtendedPolicy, PromotionExtendedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishIntegrationEventPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionExtendedPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new PromotionExtendedIntegrationEvent(
            notification.DomainEvent.PromotionId,
            notification.DomainEvent.Months);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}