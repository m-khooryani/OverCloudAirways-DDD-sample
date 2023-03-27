using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;

internal class PublishOrderCanceledPolicyHandler : IDomainPolicyHandler<OrderCanceledPolicy, OrderCanceledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishOrderCanceledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderCanceledPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new OrderCanceledIntegrationEvent(notification.DomainEvent.OrderId);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}