using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Application.Products.Policies.Created;

internal class EnqueueProjectingReadModelProductCreatedPolicyHandler : IDomainPolicyHandler<ProductCreatedPolicy, ProductCreatedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelProductCreatedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(ProductCreatedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectProductReadModelCommand(notification.DomainEvent.ProductId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
