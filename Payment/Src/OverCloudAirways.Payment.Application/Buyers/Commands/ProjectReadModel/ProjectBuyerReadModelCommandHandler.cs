using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;

internal class ProjectBuyerReadModelCommandHandler : CommandHandler<ProjectBuyerReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectBuyerReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }


    public override async Task HandleAsync(ProjectBuyerReadModelCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _aggregateRepository.LoadAsync<Buyer, BuyerId>(command.BuyerId);

        var readmodel = new BuyerReadModel(
            buyer.Id.Value,
            buyer.FirstName,
            buyer.LastName,
            buyer.Email,
            buyer.PhoneNumber);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}