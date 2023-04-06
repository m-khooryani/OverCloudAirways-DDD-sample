using OverCloudAirways.PaymentService.Application.Promotions.Policies.Launched;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Promotions;

public class PromotionLaunchedPolicyBuilder
{
    private PromotionLaunchedDomainEvent _domainEvent = new PromotionLaunchedDomainEventBuilder().Build();

    public PromotionLaunchedPolicy Build()
    {
        return new PromotionLaunchedPolicy(_domainEvent);
    }

    public PromotionLaunchedPolicyBuilder SetDomainEvent(PromotionLaunchedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}