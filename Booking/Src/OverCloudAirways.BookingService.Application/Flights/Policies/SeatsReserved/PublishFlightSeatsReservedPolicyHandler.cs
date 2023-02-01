using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.IntegrationEvents.Flights;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;

internal class PublishFlightSeatsReservedPolicyHandler : IDomainPolicyHandler<FlightSeatsReservedPolicy, FlightSeatsReservedDomainEvent>
{
    private readonly IUserAccessor _userAccessor;
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishFlightSeatsReservedPolicyHandler(
        IUserAccessor userAccessor,
        ICommandsScheduler commandsScheduler)
    {
        _userAccessor = userAccessor;
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightSeatsReservedPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new FlightSeatsReservedIntegrationEvent(
            notification.DomainEvent.FlightId,
            _userAccessor.TcpConnectionId,
            notification.DomainEvent.SeatsCount);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
