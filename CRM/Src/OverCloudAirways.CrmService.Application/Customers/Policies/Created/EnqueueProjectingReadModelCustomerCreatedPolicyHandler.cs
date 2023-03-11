using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Application.Customers.Policies.Created;

internal class EnqueueProjectingReadModelCustomerCreatedPolicyHandler : IDomainPolicyHandler<CustomerCreatedPolicy, CustomerCreatedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelCustomerCreatedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(CustomerCreatedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectCustomerReadModelCommand(notification.DomainEvent.CustomerId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}