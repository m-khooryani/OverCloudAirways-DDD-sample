using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Expired;

internal class PublishOrderExpiredPolicyHandler : IDomainPolicyHandler<OrderExpiredPolicy, OrderExpiredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishOrderExpiredPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderExpiredPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new OrderExpiredIntegrationEvent(notification.DomainEvent.OrderId);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
