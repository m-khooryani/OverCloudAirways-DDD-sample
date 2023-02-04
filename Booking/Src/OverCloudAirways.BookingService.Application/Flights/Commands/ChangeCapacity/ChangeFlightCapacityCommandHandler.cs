using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeCapacity;

internal class ChangeFlightCapacityCommandHandler : CommandHandler<ChangeFlightCapacityCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public ChangeFlightCapacityCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ChangeFlightCapacityCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ChangeCapacityAsync(command.Capacity);
    }
}