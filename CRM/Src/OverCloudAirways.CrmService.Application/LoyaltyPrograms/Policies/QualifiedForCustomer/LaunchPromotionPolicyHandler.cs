using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.Launch;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Application.LoyaltyPrograms.Policies.QualifiedForCustomer;

internal class LaunchPromotionPolicyHandler :
    IDomainPolicyHandler<LoyaltyProgramQualifiedForCustomerPolicy, LoyaltyProgramQualifiedForCustomerDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public LaunchPromotionPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(LoyaltyProgramQualifiedForCustomerPolicy notification, CancellationToken cancellationToken)
    {
        var launchPromotionCommand = new LaunchPromotionCommand(
            PromotionId.New(),
            notification.DomainEvent.DiscountPercentage,
            Description: null,
            notification.DomainEvent.CustomerId);

        await _commandsScheduler.EnqueueAsync(launchPromotionCommand);
    }
}
