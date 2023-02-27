using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public class OrderId : TypedId<Guid>
{
    public OrderId(Guid value) : base(value)
    {
    }

    public static OrderId New()
    {
        return new OrderId(Guid.NewGuid());
    }
}
