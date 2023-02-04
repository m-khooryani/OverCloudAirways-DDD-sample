using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;

internal class EnqueueProjectingReadModelFlightSeatsReservedPolicyHandler : IDomainPolicyHandler<FlightSeatsReservedPolicy, FlightSeatsReservedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelFlightSeatsReservedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightSeatsReservedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectFlightReadModelCommand(notification.DomainEvent.FlightId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
