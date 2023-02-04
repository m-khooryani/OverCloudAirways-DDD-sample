using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class FlightCapacityMustBeGreaterEqualThanTotalBookedReservedSeatsRule : IBusinessRule
{
    private readonly int _capacity;
    private readonly int _bookedSeats;
    private readonly int _reservedSeats;

    public FlightCapacityMustBeGreaterEqualThanTotalBookedReservedSeatsRule(
        int capacity, 
        int bookedSeats, 
        int reservedSeats) 
    {
        _capacity = capacity;
        _bookedSeats = bookedSeats;
        _reservedSeats = reservedSeats;
    }

    public string TranslationKey => "Flight_Capacity_Must_Be_Greater_Equal_Than_Booked_Seats";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_capacity >= _bookedSeats + _reservedSeats);
    }
}
