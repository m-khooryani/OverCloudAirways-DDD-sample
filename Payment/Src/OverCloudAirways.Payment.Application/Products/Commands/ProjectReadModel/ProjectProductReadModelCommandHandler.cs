using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;

internal class ProjectProductReadModelCommandHandler : CommandHandler<ProjectProductReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectProductReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }


    public override async Task HandleAsync(ProjectProductReadModelCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _aggregateRepository.LoadAsync<Product, ProductId>(command.ProductId);

        var readmodel = new ProductReadModel(
            buyer.Id.Value,
            buyer.Name,
            buyer.Description,
            buyer.Price,
            buyer.IsEnabled);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
