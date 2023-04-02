using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Application.Models;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;

namespace OverCloudAirways.CrmService.Application.Purchases.Commands.ProjectReadModel;

internal class ProjectPurchaseReadModelCommandHandler : CommandHandler<ProjectPurchaseReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectPurchaseReadModelCommandHandler(
        ICosmosManager cosmosManager, 
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ProjectPurchaseReadModelCommand command, CancellationToken cancellationToken)
    {
        var purchase = await _aggregateRepository.LoadAsync<Purchase, PurchaseId>(command.PurchaseId);
        var customer = await _aggregateRepository.LoadAsync<Customer, CustomerId>(purchase.CustomerId);

        var readmodel = new PurchaseReadModel(
            purchase.Id.Value,
            purchase.CustomerId.Value,
            customer.FirstName,
            customer.LastName,
            purchase.Date,
            purchase.Amount);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }

    private record PurchaseReadModel(
        Guid PurchaseId,
        Guid CustomerId,
        string CustomerFirstName,
        string CustomerLastName,
        DateTimeOffset Date,
        decimal Amount) : ReadModel(PurchaseId.ToString(), CustomerId.ToString());
}
