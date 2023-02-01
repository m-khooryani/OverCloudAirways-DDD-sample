using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class FlightMustHaveEnoughAvailableSeatsToReserveRule : IBusinessRule
{
    private readonly Flight _flight;
    private readonly int _seatsToReserve;

    public FlightMustHaveEnoughAvailableSeatsToReserveRule(Flight flight, int seatsToReserve)
    {
        _flight = flight;
        _seatsToReserve = seatsToReserve;
    }

    public string TranslationKey => "Flight_Must_Have_Enough_Available_Seats_To_Reserve";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_flight.AvailableSeats >= _seatsToReserve);
    }
}
