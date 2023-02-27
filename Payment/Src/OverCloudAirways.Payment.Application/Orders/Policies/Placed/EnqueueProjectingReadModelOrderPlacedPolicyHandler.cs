using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;

internal class EnqueueProjectingReadModelOrderPlacedPolicyHandler : IDomainPolicyHandler<OrderPlacedPolicy, OrderPlacedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelOrderPlacedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderPlacedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectOrderReadModelCommand(notification.DomainEvent.OrderId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
