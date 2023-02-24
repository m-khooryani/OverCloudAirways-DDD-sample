using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Cancel;

internal class CancelFlightBookingCommandHandler : CommandHandler<CancelFlightBookingCommand>
{
    private readonly IAggregateRepository _repository;

    public CancelFlightBookingCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(CancelFlightBookingCommand command, CancellationToken cancellationToken)
    {
        var flightBooking = await _repository.LoadAsync<FlightBooking, FlightBookingId>(command.FlightBookingId);

        await flightBooking.CancelAsync(_repository);
    }
}