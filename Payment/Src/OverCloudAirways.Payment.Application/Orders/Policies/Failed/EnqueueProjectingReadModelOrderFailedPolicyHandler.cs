using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;

internal class EnqueueProjectingReadModelOrderFailedPolicyHandler : IDomainPolicyHandler<OrderFailedPolicy, OrderFailedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelOrderFailedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderFailedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectOrderReadModelCommand(notification.DomainEvent.OrderId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}