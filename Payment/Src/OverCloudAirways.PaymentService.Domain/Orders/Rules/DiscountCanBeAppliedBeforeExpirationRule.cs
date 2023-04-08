using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Domain.Orders.Rules;

internal class DiscountCanBeAppliedBeforeExpirationRule : IBusinessRule
{
    private readonly Promotion _promotion;

    public DiscountCanBeAppliedBeforeExpirationRule(Promotion promotion)
    {
        _promotion = promotion;
    }

    public string TranslationKey => "Discount_Can_Be_Applied_Before_Expiration";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_promotion.ExpirationDate >= Clock.Now);
    }
}
