using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.Application.Purchases.Policies.Made;

internal class CollectLoyaltyPointsPolicyHandler : IDomainPolicyHandler<PurchaseMadePolicy, PurchaseMadeDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public CollectLoyaltyPointsPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PurchaseMadePolicy notification, CancellationToken cancellationToken)
    {
        var command = new CollectCustomerLoyaltyPointsCommand(
            notification.DomainEvent.CustomerId,
            notification.DomainEvent.Amount);

        await _commandsScheduler.EnqueueAsync(command);
    }
}
