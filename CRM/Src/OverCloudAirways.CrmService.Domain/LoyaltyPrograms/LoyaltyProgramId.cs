using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public class LoyaltyProgramId : TypedId<Guid>
{
    public LoyaltyProgramId(Guid value) : base(value)
    {
    }

    public static LoyaltyProgramId New()
    {
        return new LoyaltyProgramId(Guid.NewGuid());
    }
}
