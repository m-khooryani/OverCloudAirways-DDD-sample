using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Flights;

public class FlightId : TypedId<Guid>
{
    public FlightId(Guid value) : base(value)
    {
    }

    public static FlightId New()
    {
        return new FlightId(Guid.NewGuid());
    }
}
