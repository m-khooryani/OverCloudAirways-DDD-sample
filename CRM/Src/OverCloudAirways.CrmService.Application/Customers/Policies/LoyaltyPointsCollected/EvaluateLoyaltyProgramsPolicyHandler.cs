using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Evaluate;
using OverCloudAirways.CrmService.Domain.Customers.Events;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsCollected;

internal class EvaluateLoyaltyProgramsPolicyHandler : IDomainPolicyHandler<CustomerLoyaltyPointsCollectedPolicy, CustomerLoyaltyPointsCollectedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;
    private readonly IActiveLoyaltyPrograms _activeLoyaltyPrograms;

    public EvaluateLoyaltyProgramsPolicyHandler(
        ICommandsScheduler commandsScheduler, 
        IActiveLoyaltyPrograms activeLoyaltyPrograms)
    {
        _commandsScheduler = commandsScheduler;
        _activeLoyaltyPrograms = activeLoyaltyPrograms;
    }

    public async Task Handle(CustomerLoyaltyPointsCollectedPolicy notification, CancellationToken cancellationToken)
    {
        var loyaltyProgramIds = await _activeLoyaltyPrograms.GetLoyaltyProgramIds();

        foreach (var loyaltyProgramId in loyaltyProgramIds)
        {
            var command = new EvaluateLoyaltyProgramCommand(
                loyaltyProgramId,
                notification.DomainEvent.CustomerId);

            await _commandsScheduler.EnqueueAsync(command);
        }
    }
}
