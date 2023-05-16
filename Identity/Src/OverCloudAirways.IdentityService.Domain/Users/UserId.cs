using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.IdentityService.Domain.Users;

public class UserId : TypedId<Guid>
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId New()
    {
        return new UserId(Guid.NewGuid());
    }
}
