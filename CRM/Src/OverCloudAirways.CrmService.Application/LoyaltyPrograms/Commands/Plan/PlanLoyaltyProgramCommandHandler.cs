using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Plan;

internal class PlanLoyaltyProgramCommandHandler : CommandHandler<PlanLoyaltyProgramCommand>
{
    private readonly IAggregateRepository _repository;
    private readonly ILoyaltyProgramNameUniqueChecker _loyaltyProgramNameUniqueChecker;

    public PlanLoyaltyProgramCommandHandler(
        IAggregateRepository repository, 
        ILoyaltyProgramNameUniqueChecker loyaltyProgramNameUniqueChecker)
    {
        _repository = repository;
        _loyaltyProgramNameUniqueChecker = loyaltyProgramNameUniqueChecker;
    }

    public override async Task HandleAsync(PlanLoyaltyProgramCommand command, CancellationToken cancellationToken)
    {
        var loyaltyProgram = await LoyaltyProgram.PlanAsync(
            _loyaltyProgramNameUniqueChecker,
            command.LoyaltyProgramId,
            command.Name,
            command.PurchaseRequirements,
            command.DiscountPercentage);

        _repository.Add(loyaltyProgram);
    }
}