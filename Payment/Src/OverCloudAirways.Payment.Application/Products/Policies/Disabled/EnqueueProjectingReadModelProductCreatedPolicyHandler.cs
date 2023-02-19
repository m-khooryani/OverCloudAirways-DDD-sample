using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Disabled;

internal class EnqueueProjectingReadModelProductDisabledPolicyHandler : IDomainPolicyHandler<ProductDisabledPolicy, ProductDisabledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelProductDisabledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(ProductDisabledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectProductReadModelCommand(notification.DomainEvent.ProductId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
