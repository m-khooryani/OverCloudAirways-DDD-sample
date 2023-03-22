using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Application.Promotions.Commands.Launch;

internal class LaunchPromotionCommandHandler : CommandHandler<LaunchPromotionCommand>
{
    private readonly IAggregateRepository _repository;
    private readonly IDiscountCodeGenerator _discountCodeGenerator;

    public LaunchPromotionCommandHandler(
        IAggregateRepository repository, 
        IDiscountCodeGenerator discountCodeGenerator)
    {
        _repository = repository;
        _discountCodeGenerator = discountCodeGenerator;
    }

    public override async Task HandleAsync(LaunchPromotionCommand command, CancellationToken cancellationToken)
    {
        var promotion = Promotion.Launch(
            _discountCodeGenerator,
            command.PromotionId,
            command.DiscountPercentage,
            command.Description,
            command.CustomerId);

        _repository.Add(promotion);

        await Task.CompletedTask;
    }
}