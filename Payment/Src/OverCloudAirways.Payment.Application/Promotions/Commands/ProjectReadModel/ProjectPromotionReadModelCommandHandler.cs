using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Application.Promotions.Commands.ProjectReadModel;

internal class ProjectPromotionReadModelCommandHandler : CommandHandler<ProjectPromotionReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectPromotionReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectPromotionReadModelCommand command, CancellationToken cancellationToken)
    {
        var promotion = await _aggregateRepository.LoadAsync<Promotion, PromotionId>(command.PromotionId);
        var customer = promotion.BuyerId is not null ?
            await _aggregateRepository.LoadAsync<Buyer, BuyerId>(promotion.BuyerId) :
            null;

        var readmodel = new PromotionReadModel(
            promotion.Id.Value,
            promotion.DiscountCode,
            promotion.DiscountPercentage,
            promotion.Description,
            promotion.ExpirationDate,
            customer?.FirstName,
            customer?.LastName);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
