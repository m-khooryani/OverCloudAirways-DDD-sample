using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Tickets;

public class TicketId : TypedId<Guid>
{
    public TicketId(Guid value) : base(value)
    {
    }

    public static TicketId New()
    {
        return new TicketId(Guid.NewGuid());
    }
}
