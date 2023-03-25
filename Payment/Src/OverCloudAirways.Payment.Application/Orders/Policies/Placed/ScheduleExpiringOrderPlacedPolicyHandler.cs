using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Application.Orders.Commands.Expire;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;

internal class ScheduleExpiringOrderPlacedPolicyHandler : IDomainPolicyHandler<OrderPlacedPolicy, OrderPlacedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ScheduleExpiringOrderPlacedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderPlacedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ExpireOrderCommand(notification.DomainEvent.OrderId);
        await _commandsScheduler.ScheduleAsync(command, Clock.Now.AddMinutes(15));
    }
}
