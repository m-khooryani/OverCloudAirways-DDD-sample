using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Purchases.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Purchases.Events;

namespace OverCloudAirways.CrmService.Application.Purchases.Policies.Made;

internal class ProjectReadModelPolicyHandler : IDomainPolicyHandler<PurchaseMadePolicy, PurchaseMadeDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PurchaseMadePolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPurchaseReadModelCommand(notification.DomainEvent.PurchaseId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
