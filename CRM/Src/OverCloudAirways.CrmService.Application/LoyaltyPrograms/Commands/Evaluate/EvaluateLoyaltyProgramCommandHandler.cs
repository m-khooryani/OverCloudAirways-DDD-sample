using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Evaluate;

internal class EvaluateLoyaltyProgramCommandHandler : CommandHandler<EvaluateLoyaltyProgramCommand>
{
    private readonly IAggregateRepository _repository;

    public EvaluateLoyaltyProgramCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(EvaluateLoyaltyProgramCommand command, CancellationToken cancellationToken)
    {
        var loyaltyProgram = await _repository.LoadAsync<LoyaltyProgram, LoyaltyProgramId>(command.LoyaltyProgramId);
        var customer = await _repository.LoadAsync<Customer, CustomerId>(command.CustomerId);

        loyaltyProgram.Evaluate(customer);
    }
}