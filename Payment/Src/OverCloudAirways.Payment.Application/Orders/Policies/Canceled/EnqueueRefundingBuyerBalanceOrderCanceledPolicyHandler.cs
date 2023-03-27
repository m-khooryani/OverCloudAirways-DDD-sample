using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;

internal class EnqueueRefundingBuyerBalanceOrderCanceledPolicyHandler : IDomainPolicyHandler<OrderCanceledPolicy, OrderCanceledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueRefundingBuyerBalanceOrderCanceledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderCanceledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new RefundBuyerBalanceCommand(
            notification.DomainEvent.BuyerId, 
            notification.DomainEvent.TotalAmount);

        await _commandsScheduler.EnqueueAsync(command);
    }
}
