using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Rules;

internal class FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule : IBusinessRule
{
    private readonly IAggregateRepository _repository;
    private readonly FlightId _flightId;

    public FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule(
        IAggregateRepository repository,
        FlightId flightId)
    {
        _repository = repository;
        _flightId = flightId;
    }

    public string TranslationKey => "FlightBooking_Can_Only_Be_Cancelled_For_Flights_Has_Not_Yet_Departed";

    public async Task<bool> IsFollowedAsync()
    {
        var flight = await _repository.LoadAsync<Flight, FlightId>(_flightId);
        return flight.Status.HasNotYetDeparted();
    }
}
