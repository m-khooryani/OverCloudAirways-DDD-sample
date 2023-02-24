using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Confirm;

internal class ConfirmFlightBookingCommandHandler : CommandHandler<ConfirmFlightBookingCommand>
{
    private readonly IAggregateRepository _repository;

    public ConfirmFlightBookingCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ConfirmFlightBookingCommand command, CancellationToken cancellationToken)
    {
        var flightBooking = await _repository.LoadAsync<FlightBooking, FlightBookingId>(command.FlightBookingId);

        await flightBooking.ConfirmAsync(_repository);
    }
}