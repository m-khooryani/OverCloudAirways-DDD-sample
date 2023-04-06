using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.TestHelpers.Promotions;

public class PromotionBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private string _discountCode = "OA_123";
    private Percentage _discountPercentage = Percentage.Of(20M);
    private string _description = "New year discount";
    private BuyerId _buyerId = BuyerId.New();
    private DateTimeOffset _expirationDate = Clock.Now.AddYears(1);

    public Promotion Build()
    {
        return Promotion.Launch(
            _promotionId,
            _discountCode,
            _discountPercentage,
            _description,
            _buyerId,
            _expirationDate);
    }

    public PromotionBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public PromotionBuilder SetDiscountCode(string discountCode) 
    {
        _discountCode = discountCode;
        return this;
    }

    public PromotionBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public PromotionBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public PromotionBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public PromotionBuilder SetExpirationDate(DateTimeOffset expirationDate)
    {
        _expirationDate = expirationDate;
        return this;
    }
}