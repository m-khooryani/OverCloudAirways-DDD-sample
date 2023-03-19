using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Suspend;

internal class SuspendLoyaltyProgramCommandHandler : CommandHandler<SuspendLoyaltyProgramCommand>
{
    private readonly IAggregateRepository _repository;

    public SuspendLoyaltyProgramCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(SuspendLoyaltyProgramCommand command, CancellationToken cancellationToken)
    {
        var loyaltyProgram = await _repository.LoadAsync<LoyaltyProgram, LoyaltyProgramId>(command.LoyaltyProgramId);
        loyaltyProgram.Suspend();
    }
}