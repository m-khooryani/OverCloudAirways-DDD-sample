using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Application.Promotions.Commands.Launch;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;

namespace OverCloudAirways.PaymentService.TestHelpers.Promotions;

public class LaunchPromotionCommandBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private string _discountCode = "OA_123";
    private Percentage _discountPercentage = Percentage.Of(20M);
    private string _description = "New year discount";
    private BuyerId? _buyerId = BuyerId.New();
    private DateTimeOffset _expirationDate = Clock.Now.AddYears(1);

    public LaunchPromotionCommand Build()
    {
        return new LaunchPromotionCommand(
            _promotionId,
            _discountCode,
            _discountPercentage,
            _description,
            _buyerId,
            _expirationDate);
    }

    public LaunchPromotionCommandBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public LaunchPromotionCommandBuilder SetDiscountCode(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public LaunchPromotionCommandBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public LaunchPromotionCommandBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public LaunchPromotionCommandBuilder SetBuyerId(BuyerId? buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public LaunchPromotionCommandBuilder SetExpirationDate(DateTimeOffset expirationDate)
    {
        _expirationDate = expirationDate;
        return this;
    }
}