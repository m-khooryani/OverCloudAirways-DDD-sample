using OverCloudAirways.CrmService.Application.Promotions.Policies.Extended;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Promotions;

public class PromotionExtendedPolicyBuilder
{
    private PromotionExtendedDomainEvent _domainEvent = new PromotionExtendedDomainEventBuilder().Build();

    public PromotionExtendedPolicy Build()
    {
        return new PromotionExtendedPolicy(_domainEvent);
    }

    public PromotionExtendedPolicyBuilder SetDomainEvent(PromotionExtendedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
