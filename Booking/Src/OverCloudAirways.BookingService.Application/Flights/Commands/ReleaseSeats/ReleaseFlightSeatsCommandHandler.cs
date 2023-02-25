using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReleaseSeats;

internal class ReleaseFlightSeatsCommandHandler : CommandHandler<ReleaseFlightSeatsCommand>
{
    private readonly IAggregateRepository _repository;

    public ReleaseFlightSeatsCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ReleaseFlightSeatsCommand command, CancellationToken cancellationToken)
    {
        var flight = await _repository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ReleaseSeatsAsync(command.SeatsCount);
    }
}