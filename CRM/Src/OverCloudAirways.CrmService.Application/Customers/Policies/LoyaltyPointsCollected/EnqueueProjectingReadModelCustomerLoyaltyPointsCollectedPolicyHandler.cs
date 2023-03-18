using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsCollected;

internal class EnqueueProjectingReadModelCustomerLoyaltyPointsCollectedPolicyHandler : IDomainPolicyHandler<CustomerLoyaltyPointsCollectedPolicy, CustomerLoyaltyPointsCollectedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelCustomerLoyaltyPointsCollectedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(CustomerLoyaltyPointsCollectedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectCustomerReadModelCommand(notification.DomainEvent.CustomerId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
