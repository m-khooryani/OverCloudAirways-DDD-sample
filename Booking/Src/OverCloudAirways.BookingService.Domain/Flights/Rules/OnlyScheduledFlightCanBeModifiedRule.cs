using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class OnlyScheduledFlightCanBeModifiedRule : IBusinessRule
{
    private readonly Flight _flight;

    public OnlyScheduledFlightCanBeModifiedRule(Flight flight)
    {
        _flight = flight;
    }

    public string TranslationKey => "Only_Scheduled_Flight_Can_Be_Modified";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_flight.Status == FlightStatus.Scheduled);
    }
}
