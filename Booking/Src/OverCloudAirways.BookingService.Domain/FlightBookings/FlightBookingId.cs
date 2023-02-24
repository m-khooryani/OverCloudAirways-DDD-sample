using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.FlightBookings;

public class FlightBookingId : TypedId<Guid>
{
    public FlightBookingId(Guid value) : base(value)
    {
    }

    public static FlightBookingId New()
    {
        return new FlightBookingId(Guid.NewGuid());
    }
}