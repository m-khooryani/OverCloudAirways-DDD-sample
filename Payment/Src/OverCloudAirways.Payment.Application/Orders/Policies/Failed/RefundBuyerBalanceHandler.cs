using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;

internal class RefundBuyerBalanceHandler : IDomainPolicyHandler<OrderFailedPolicy, OrderFailedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public RefundBuyerBalanceHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderFailedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new RefundBuyerBalanceCommand(
            notification.DomainEvent.BuyerId,
            notification.DomainEvent.PaidAmount);

        await _commandsScheduler.EnqueueAsync(command);
    }
}
