using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Commands.Reserve;

class ReserveFlightBookingCommandHandler : CommandHandler<ReserveFlightBookingCommand>
{
    private readonly IAggregateRepository _repository;

    public ReserveFlightBookingCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ReserveFlightBookingCommand command, CancellationToken cancellationToken)
    {
        var flight = await _repository.LoadAsync<Flight, FlightId>(command.FlightId);
        var customer = await _repository.LoadAsync<Customer, CustomerId>(command.CustomerId);

        var flightBooking = await FlightBooking.ReserveAsync(
            command.FlightBookingId,
            customer.Id,
            flight,
            command.Passengers);

        _repository.Add(flightBooking);
    }
}
