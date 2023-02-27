using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Place;

class PlaceOrderCommandHandler : CommandHandler<PlaceOrderCommand>
{
    private readonly IAggregateRepository _repository;

    public PlaceOrderCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(PlaceOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await Order.PlaceAsync(
            _repository,
            command.OrderId,
            command.BuyerId,
            command.OrderItems);

        _repository.Add(order);
    }
}