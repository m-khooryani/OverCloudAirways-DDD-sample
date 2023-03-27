using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;

internal class ReserveFlightSeatsCommandHandler : CommandHandler<ReserveFlightSeatsCommand>
{
    private readonly IUserAccessor _userAccessor;
    private readonly IAggregateRepository _aggregateRepository;

    public ReserveFlightSeatsCommandHandler(
        IUserAccessor userAccessor,
        IAggregateRepository aggregateRepository)
    {
        _userAccessor = userAccessor;
        _aggregateRepository = aggregateRepository;
    }

    public override async Task HandleAsync(ReserveFlightSeatsCommand command, CancellationToken cancellationToken)
    {
        var flight = await _aggregateRepository.LoadAsync<Flight, FlightId>(command.FlightId);

        await flight.ReserveSeatsAsync(
            new CustomerId(_userAccessor.UserId),
            command.Passengers);
    }
}