using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;

internal class EnqueueProjectingReadModelOrderCanceledPolicyHandler : IDomainPolicyHandler<OrderCanceledPolicy, OrderCanceledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelOrderCanceledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderCanceledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectOrderReadModelCommand(notification.DomainEvent.OrderId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
