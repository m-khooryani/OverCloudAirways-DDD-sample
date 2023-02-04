using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.Canceled;

internal class EnqueueProjectingReadModelFlightCanceledPolicyHandler : IDomainPolicyHandler<FlightCanceledPolicy, FlightCanceledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelFlightCanceledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightCanceledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectFlightReadModelCommand(notification.DomainEvent.FlightId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}