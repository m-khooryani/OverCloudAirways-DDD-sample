using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Reactivate;

internal class ReactivateLoyaltyProgramCommandHandler : CommandHandler<ReactivateLoyaltyProgramCommand>
{
    private readonly IAggregateRepository _repository;

    public ReactivateLoyaltyProgramCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ReactivateLoyaltyProgramCommand command, CancellationToken cancellationToken)
    {
        var loyaltyProgram = await _repository.LoadAsync<LoyaltyProgram, LoyaltyProgramId>(command.LoyaltyProgramId);
        loyaltyProgram.Reactivate();
    }
}