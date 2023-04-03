using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ResetLoyaltyPoints;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Policies.QualifiedForCustomer;

internal class ResetCustomerLoyaltyPointsPolicyHandler :
    IDomainPolicyHandler<LoyaltyProgramQualifiedForCustomerPolicy, LoyaltyProgramQualifiedForCustomerDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ResetCustomerLoyaltyPointsPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(LoyaltyProgramQualifiedForCustomerPolicy notification, CancellationToken cancellationToken)
    {
        var resetPointsCommand = new ResetCustomerLoyaltyPointsCommand(notification.DomainEvent.CustomerId);

        await _commandsScheduler.EnqueueAsync(resetPointsCommand);
    }
}