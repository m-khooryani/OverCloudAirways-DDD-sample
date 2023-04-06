using OverCloudAirways.PaymentService.Application.Promotions.Policies.Extended;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Promotions;

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
