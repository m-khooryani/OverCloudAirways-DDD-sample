using OverCloudAirways.CrmService.Domain.Promotions;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Promotions;

public class PromotionExtendedDomainEventBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private int _months = 2;

    public PromotionExtendedDomainEvent Build()
    {
        return new PromotionExtendedDomainEvent(_promotionId, _months);
    }

    public PromotionExtendedDomainEventBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public PromotionExtendedDomainEventBuilder SetMonths(int months)
    {
        _months = months;
        return this;
    }
}
