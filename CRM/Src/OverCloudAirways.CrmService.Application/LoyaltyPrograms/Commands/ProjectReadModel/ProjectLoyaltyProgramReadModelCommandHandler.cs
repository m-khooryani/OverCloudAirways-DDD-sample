using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure.CosmosDB;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.ProjectReadModel;

internal class ProjectLoyaltyProgramReadModelCommandHandler : CommandHandler<ProjectLoyaltyProgramReadModelCommand>
{
    private readonly ICosmosManager _cosmosManager;
    private readonly IAggregateRepository _aggregateRepository;

    public ProjectLoyaltyProgramReadModelCommandHandler(
        ICosmosManager cosmosManager,
        IAggregateRepository aggregateRepository)
    {
        _cosmosManager = cosmosManager;
        _aggregateRepository = aggregateRepository;
    }


    public override async Task HandleAsync(ProjectLoyaltyProgramReadModelCommand command, CancellationToken cancellationToken)
    {
        var loyaltyProgram = await _aggregateRepository.LoadAsync<LoyaltyProgram, LoyaltyProgramId>(command.LoyaltyProgramId);

        var readmodel = new LoyaltyProgramReadModel(
            loyaltyProgram.Id.Value,
            loyaltyProgram.Name,
            loyaltyProgram.PurchaseRequirements,
            loyaltyProgram.DiscountPercentage,
            loyaltyProgram.IsSuspended);

        await _cosmosManager.UpsertAsync(ContainersConstants.ReadModels, readmodel);
    }
}
