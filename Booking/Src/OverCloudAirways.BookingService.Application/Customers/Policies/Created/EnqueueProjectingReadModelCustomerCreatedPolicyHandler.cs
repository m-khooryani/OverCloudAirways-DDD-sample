using OverCloudAirways.BookingService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Customers.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Customers.Policies.Created;

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