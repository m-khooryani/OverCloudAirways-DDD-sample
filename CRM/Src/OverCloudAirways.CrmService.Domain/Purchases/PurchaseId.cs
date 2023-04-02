using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.CrmService.Domain.Purchases;

public class PurchaseId : TypedId<Guid>
{
    public PurchaseId(Guid value) : base(value)
    {
    }

    public static PurchaseId New()
    {
        return new PurchaseId(Guid.NewGuid());
    }
}