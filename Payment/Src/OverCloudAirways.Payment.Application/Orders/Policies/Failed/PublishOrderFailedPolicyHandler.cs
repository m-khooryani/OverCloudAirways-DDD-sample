using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;

internal class PublishOrderFailedPolicyHandler : IDomainPolicyHandler<OrderFailedPolicy, OrderFailedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishOrderFailedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderFailedPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new OrderFailedIntegrationEvent(notification.DomainEvent.OrderId);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
