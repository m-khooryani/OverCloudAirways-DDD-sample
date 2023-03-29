using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.Cancel;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Disabled;

internal class EnqueueCancelingConnectedOrdersProductDisabledPolicyHandler : IDomainPolicyHandler<ProductDisabledPolicy, ProductDisabledDomainEvent>
{
    private readonly IConnectedOrders _connectedOrders;
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueCancelingConnectedOrdersProductDisabledPolicyHandler(
        IConnectedOrders connectedOrders,
        ICommandsScheduler commandsScheduler)
    {
        _connectedOrders = connectedOrders;
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(ProductDisabledPolicy notification, CancellationToken cancellationToken)
    {
        var connectedOrderIds = await _connectedOrders.GetConnectedOrderIds(notification.DomainEvent.ProductId);

        foreach (var orderId in connectedOrderIds)
        {
            await _commandsScheduler.EnqueueAsync(new CancelOrderCommand(orderId));
        }
    }
}
