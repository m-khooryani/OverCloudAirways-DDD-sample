using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.Application.Promotions.Policies.Launched;

internal class ProjectReadModelPolicyHandler : IDomainPolicyHandler<PromotionLaunchedPolicy, PromotionLaunchedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionLaunchedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPromotionReadModelCommand(notification.DomainEvent.PromotionId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
