using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Rules;

internal class FlightBookingCanOnlyBeReservedForFlightsHasNotYetDepartedRule : IBusinessRule
{
    private readonly Flight _flight;

    public FlightBookingCanOnlyBeReservedForFlightsHasNotYetDepartedRule(Flight flight)
    {
        _flight = flight;
    }

    public string TranslationKey => "FlightBooking_Can_Only_Be_Reserved_For_Flights_Has_Not_Yet_Departed";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_flight.Status.HasNotYetDeparted());
    }
}
