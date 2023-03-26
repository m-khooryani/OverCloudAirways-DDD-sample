using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Confirmed;

internal class EnqueueProjectingReadModelOrderConfirmedPolicyHandler : IDomainPolicyHandler<OrderConfirmedPolicy, OrderConfirmedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelOrderConfirmedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderConfirmedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectOrderReadModelCommand(notification.DomainEvent.OrderId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}