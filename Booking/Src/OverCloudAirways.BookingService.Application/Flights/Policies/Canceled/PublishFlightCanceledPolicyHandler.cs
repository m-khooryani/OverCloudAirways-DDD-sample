using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.IntegrationEvents.Flights;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.Canceled;

internal class PublishFlightCanceledPolicyHandler : IDomainPolicyHandler<FlightCanceledPolicy, FlightCanceledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishFlightCanceledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightCanceledPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new FlightCanceledIntegrationEvent(notification.DomainEvent.FlightId);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
