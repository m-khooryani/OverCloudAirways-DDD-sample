using OverCloudAirways.BookingService.Application.Flights.Commands.ReleaseSeats;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Policies.Cancelled;

internal class EnqueueReleaseFlightSeatsFlightBookingCancelledPolicyHandler : IDomainPolicyHandler<FlightBookingCancelledPolicy, FlightBookingCancelledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueReleaseFlightSeatsFlightBookingCancelledPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightBookingCancelledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ReleaseFlightSeatsCommand(
            notification.DomainEvent.FlightId,
            notification.DomainEvent.SeatsCount);

        await _commandsScheduler.EnqueueAsync(command);
    }
}
