using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Buyers;

public class BuyerId : TypedId<Guid>
{
    public BuyerId(Guid value) : base(value)
    {
    }

    public static BuyerId New()
    {
        return new BuyerId(Guid.NewGuid());
    }
}
