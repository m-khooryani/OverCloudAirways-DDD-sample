using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Cancel;

internal class CancelOrderCommandHandler : CommandHandler<CancelOrderCommand>
{
    private readonly IAggregateRepository _repository;

    public CancelOrderCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.LoadAsync<Order, OrderId>(command.OrderId);
        await order.CancelAsync();
    }
}