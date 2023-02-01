using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;

internal class ReserveFlightSeatsCommandHandler : CommandHandler<ReserveFlightSeatsCommand>
{
    private readonly IAggregateRepository _aggregateRepository;

    public ReserveFlightSeatsCommandHandler(IAggregateRepository aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ReserveFlightSeatsCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ReserveSeatsAsync(command.SeatsCount);
    }
}