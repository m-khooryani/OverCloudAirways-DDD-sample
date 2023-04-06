using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Promotions;

public class PromotionLaunchedDomainEventBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private string _discountCode = "OA_10023";
    private Percentage _discountPercentage = Percentage.Of(20M);
    private string _description = "New year discount";
    private BuyerId _buyerId = BuyerId.New();
    private DateTimeOffset _expirationDate = Clock.Now.AddYears(1);

    public PromotionLaunchedDomainEvent Build()
    {
        return new PromotionLaunchedDomainEvent(
            _promotionId,
            _discountCode,
            _discountPercentage,
            _description,
            _buyerId,
            _expirationDate);
    }

    public PromotionLaunchedDomainEventBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDiscountCode(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetExpirationDate(DateTimeOffset expirationDate)
    {
        _expirationDate = expirationDate;
        return this;
    }
}
