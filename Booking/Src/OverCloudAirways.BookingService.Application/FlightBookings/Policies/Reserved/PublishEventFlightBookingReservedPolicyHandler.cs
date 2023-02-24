using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.IntegrationEvents.FlightBookings;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Policies.Reserved;

internal class PublishEventFlightBookingReservedPolicyHandler : IDomainPolicyHandler<FlightBookingReservedPolicy, FlightBookingReservedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PublishEventFlightBookingReservedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightBookingReservedPolicy notification, CancellationToken cancellationToken)
    {
        var @event = new FlightBookingReservedIntegrationEvent(
            notification.DomainEvent.FlightBookingId,
            notification.DomainEvent.CustomerId,
            notification.DomainEvent.FlightId,
            notification.DomainEvent.Passengers);

        await _commandsScheduler.EnqueuePublishingEventAsync(@event);
    }
}
