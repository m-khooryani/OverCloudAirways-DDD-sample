using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Airports;

public class AirportId : TypedId<Guid>
{
    public AirportId(Guid value) : base(value)
    {
    }

    public static AirportId New()
    {
        return new AirportId(Guid.NewGuid());
    }
}
