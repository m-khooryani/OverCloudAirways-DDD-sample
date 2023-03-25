using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Expire;

internal class ExpireOrderCommandHandler : CommandHandler<ExpireOrderCommand>
{
    private readonly IAggregateRepository _repository;

    public ExpireOrderCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ExpireOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.LoadAsync<Order, OrderId>(command.OrderId);
        await order.ExpireAsync();
    }
}
