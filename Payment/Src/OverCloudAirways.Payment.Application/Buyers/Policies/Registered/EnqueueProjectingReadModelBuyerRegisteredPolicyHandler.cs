using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;

namespace OverCloudAirways.PaymentService.Application.Buyers.Policies.Registered;

internal class EnqueueProjectingReadModelBuyerRegisteredPolicyHandler : IDomainPolicyHandler<BuyerRegisteredPolicy, BuyerRegisteredDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelBuyerRegisteredPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(BuyerRegisteredPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectBuyerReadModelCommand(notification.DomainEvent.BuyerId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
