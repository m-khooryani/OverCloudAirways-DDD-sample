using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BookingService.Domain.Flights.Rules;

internal class FlightCanOnlyBeScheduledInTheFutureRule : IBusinessRule
{
    private readonly DateTimeOffset _departureTime;

    public FlightCanOnlyBeScheduledInTheFutureRule(DateTimeOffset departureTime)
    {
        _departureTime = departureTime;
    }

    public string TranslationKey => "Flight_Can_Only_Be_Scheduled_In_The_Future";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_departureTime > Clock.Now);
    }
}
