using OverCloudAirways.BookingService.Application.Flights.Commands.ChargePrice;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.Scheduled;

internal class EnqueueChargingFlightPricePolicyHandler : IDomainPolicyHandler<FlightScheduledPolicy, FlightScheduledDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueChargingFlightPricePolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(FlightScheduledPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ChargeFlightPriceCommand(notification.DomainEvent.FlightId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
