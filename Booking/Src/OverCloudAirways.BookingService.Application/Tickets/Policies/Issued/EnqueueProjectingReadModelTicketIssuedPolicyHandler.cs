using OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Tickets.Policies.Issued;

internal class EnqueueProjectingReadModelTicketIssuedPolicyHandler : IDomainPolicyHandler<TicketIssuedPolicy, TicketIssuedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelTicketIssuedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(TicketIssuedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectTicketReadModelCommand(notification.DomainEvent.TicketId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}