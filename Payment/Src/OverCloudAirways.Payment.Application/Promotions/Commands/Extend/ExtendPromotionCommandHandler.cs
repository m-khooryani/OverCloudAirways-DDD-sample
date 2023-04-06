using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.Extend;

internal class ExtendPromotionCommandHandler : CommandHandler<ExtendPromotionCommand>
{
    private readonly IAggregateRepository _repository;

    public ExtendPromotionCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ExtendPromotionCommand command, CancellationToken cancellationToken)
    {
        var promotion = await _repository.LoadAsync<Promotion, PromotionId>(command.PromotionId);

        promotion.Extend(command.Months);
    }
}