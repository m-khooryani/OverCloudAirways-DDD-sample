using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Payments;

public class PaymentId : TypedId<Guid>
{
    public PaymentId(Guid value) : base(value)
    {
    }

    public static PaymentId New()
    {
        return new PaymentId(Guid.NewGuid());
    }
}