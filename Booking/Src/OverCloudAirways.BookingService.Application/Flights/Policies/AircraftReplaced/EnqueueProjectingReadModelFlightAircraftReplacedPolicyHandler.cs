using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.AircraftReplaced;

internal class EnqueueProjectingReadModelFlightAircraftReplacedPolicyHandler : IDomainPolicyHandler<FlightAircraftReplacedPolicy, FlightAircraftReplacedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelFlightAircraftReplacedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightAircraftReplacedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectFlightReadModelCommand(notification.DomainEvent.FlightId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}