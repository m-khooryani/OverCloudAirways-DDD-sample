using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.CrmService.Domain.Customers;

public class CustomerId : TypedId<Guid>
{
    public CustomerId(Guid value) : base(value)
    {
    }

    public static CustomerId New()
    {
        return new CustomerId(Guid.NewGuid());
    }
}
