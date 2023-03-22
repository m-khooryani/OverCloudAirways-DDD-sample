using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.Infrastructure.DomainServices.Promotions;

internal class DiscountCodeGenerator : IDiscountCodeGenerator
{
    public string Generate()
    {
        // can be more complicated/custom logic
        return Guid.NewGuid().ToString();
    }
}
