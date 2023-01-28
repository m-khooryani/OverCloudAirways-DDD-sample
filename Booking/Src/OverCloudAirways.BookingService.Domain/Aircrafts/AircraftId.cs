using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Aircrafts;

public class AircraftId : TypedId<Guid>
{
	public AircraftId(Guid value)
		: base(value)
	{
	}

	public static AircraftId New()
	{
		return new AircraftId(Guid.NewGuid());
	}
}
