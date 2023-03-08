using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Products;

public class ProductId : TypedId<Guid>
{
    public ProductId(Guid value) : base(value)
    {
    }

    public static ProductId New()
    {
        return new ProductId(Guid.NewGuid());
    }
}
