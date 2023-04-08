using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.Domain.Orders.Rules;

internal class DiscountCanBeAppliedForOrderWithValidBuyerRule : IBusinessRule
{
    private readonly BuyerId _buyerId;
    private readonly Promotion _promotion;

    public DiscountCanBeAppliedForOrderWithValidBuyerRule(
        BuyerId buyerId,
        Promotion promotion)
    {
        _buyerId = buyerId;
        _promotion = promotion;
    }

    public string TranslationKey => "Discount_Can_Be_Applied_For_Order_With_Valid_Customer";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_promotion.BuyerId is null || _promotion.BuyerId == _buyerId);
    }
}
