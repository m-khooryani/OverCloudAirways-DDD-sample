using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.ApplyDiscount;

internal class ApplyOrderDiscountCommandHandler : CommandHandler<ApplyOrderDiscountCommand>
{
    private readonly IAggregateRepository _repository;

    public ApplyOrderDiscountCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ApplyOrderDiscountCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.LoadAsync<Order, OrderId>(command.OrderId);
        var promotion = await _repository.LoadAsync<Promotion, PromotionId>(command.PromotionId);

        await order.ApplyDiscountAsync(promotion);
    }
}
