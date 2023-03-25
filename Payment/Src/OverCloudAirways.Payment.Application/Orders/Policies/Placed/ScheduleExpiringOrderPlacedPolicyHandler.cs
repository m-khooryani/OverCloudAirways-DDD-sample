using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Application.Orders.Commands.Expire;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;

internal class ScheduleExpiringOrderPlacedPolicyHandler : IDomainPolicyHandler<OrderPlacedPolicy, OrderPlacedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;
    private readonly OrderExpirySettings _orderExpirySettings;

    public ScheduleExpiringOrderPlacedPolicyHandler(
        ICommandsScheduler commandsScheduler, 
        OrderExpirySettings orderExpirySettings)
    {
        _commandsScheduler = commandsScheduler;
        _orderExpirySettings = orderExpirySettings;
    }

    public async Task Handle(OrderPlacedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ExpireOrderCommand(notification.DomainEvent.OrderId);
        var expiryDate = Clock.Now.AddMinutes(_orderExpirySettings.ExpiryDurationInMinutes);

        await _commandsScheduler.ScheduleAsync(command, expiryDate);
    }
}
