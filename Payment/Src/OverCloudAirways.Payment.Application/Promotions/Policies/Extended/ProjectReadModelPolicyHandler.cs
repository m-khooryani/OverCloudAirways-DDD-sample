using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.Application.Promotions.Policies.Extended;

internal class ProjectReadModelPolicyHandler : IDomainPolicyHandler<PromotionExtendedPolicy, PromotionExtendedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public ProjectReadModelPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PromotionExtendedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPromotionReadModelCommand(notification.DomainEvent.PromotionId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
