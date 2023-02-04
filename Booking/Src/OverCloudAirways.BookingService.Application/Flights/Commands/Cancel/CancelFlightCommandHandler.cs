using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.Cancel;

internal class CancelFlightCommandHandler : CommandHandler<CancelFlightCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public CancelFlightCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(CancelFlightCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.CancelAsync();
    }
}
