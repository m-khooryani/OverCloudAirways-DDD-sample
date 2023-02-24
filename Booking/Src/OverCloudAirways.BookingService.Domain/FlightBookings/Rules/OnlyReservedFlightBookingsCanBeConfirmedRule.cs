using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.FlightBookings.Rules;

internal class OnlyReservedFlightBookingsCanBeConfirmedRule : IBusinessRule
{
    private readonly FlightBookingStatus _status;

    public OnlyReservedFlightBookingsCanBeConfirmedRule(FlightBookingStatus status)
    {
        _status = status;
    }

    public string TranslationKey => "Only_Reserved_FlightBookings_Can_Be_Confirmed";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_status == FlightBookingStatus.Reserved);
    }
}
