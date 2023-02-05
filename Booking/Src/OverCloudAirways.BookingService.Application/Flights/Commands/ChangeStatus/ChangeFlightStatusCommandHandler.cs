using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ChangeStatus;

internal class ChangeFlightStatusCommandHandler : CommandHandler<ChangeFlightStatusCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public ChangeFlightStatusCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ChangeFlightStatusCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        flight.ChangeStatus(command.Status);
    }
}