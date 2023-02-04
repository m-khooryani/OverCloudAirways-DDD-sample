using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReplaceAircraft;

internal class ReplaceFlightAircraftCommandHandler : CommandHandler<ReplaceFlightAircraftCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public ReplaceFlightAircraftCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ReplaceFlightAircraftCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ReplaceAircraftAsync(_aggregateRepository, command.AircraftId);
    }
}