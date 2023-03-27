using OverCloudAirways.BookingService.Application.FlightBookings.Commands.Reserve;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;

internal class EnqueueReservingFlightBookingFlightSeatsReservedPolicyHandler : IDomainPolicyHandler<FlightSeatsReservedPolicy, FlightSeatsReservedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueReservingFlightBookingFlightSeatsReservedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightSeatsReservedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ReserveFlightBookingCommand(
            FlightBookingId.New(),
            notification.DomainEvent.CustomerId,
            notification.DomainEvent.FlightId,
            notification.DomainEvent.Passengers);

        await _commandsScheduler.EnqueueAsync(command);
    }
}
