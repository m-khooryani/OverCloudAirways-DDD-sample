using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.Launch;

internal class LaunchPromotionCommandHandler : CommandHandler<LaunchPromotionCommand>
{
    private readonly IAggregateRepository _repository;

    public LaunchPromotionCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override Task HandleAsync(LaunchPromotionCommand command, CancellationToken cancellationToken)
    {
        var promotion = Promotion.Launch(
            command.PromotionId,
            command.DiscountCode,
            command.DiscountPercentage,
            command.Description,
            command.BuyerId,
            command.ExpirationDate);

        _repository.Add(promotion);

        return Task.CompletedTask;
    }
}
