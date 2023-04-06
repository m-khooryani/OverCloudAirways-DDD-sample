using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Promotions.Events;
using OverCloudAirways.CrmService.IntegrationEvents.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Policies.Launched;

internal class PublishIntegrationEventPolicyHandler : IDomainPolicyHandler<PromotionLaunchedPolicy, PromotionLaunchedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishIntegrationEventPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionLaunchedPolicy notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        var @event = new PromotionLaunchedIntegrationEvent(
            domainEvent.PromotionId,
            domainEvent.DiscountCode,
            domainEvent.DiscountPercentage,
            domainEvent.Description,
            domainEvent.CustomerId,
            domainEvent.ExpirationDate);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
