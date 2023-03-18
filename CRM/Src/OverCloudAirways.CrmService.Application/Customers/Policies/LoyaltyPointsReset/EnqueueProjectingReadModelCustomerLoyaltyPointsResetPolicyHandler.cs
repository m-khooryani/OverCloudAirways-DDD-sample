using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsReset;

internal class EnqueueProjectingReadModelCustomerLoyaltyPointsResetPolicyHandler : IDomainPolicyHandler<CustomerLoyaltyPointsResetPolicy, CustomerLoyaltyPointsResetDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelCustomerLoyaltyPointsResetPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(CustomerLoyaltyPointsResetPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectCustomerReadModelCommand(notification.DomainEvent.CustomerId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
