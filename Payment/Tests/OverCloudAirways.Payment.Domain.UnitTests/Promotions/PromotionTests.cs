using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Promotions;

public class PromotionTests : Test
{
    [Fact]
    public void LaunchPromotion_Given_Valid_Input_Should_Successfully_Launch_Promotion_And_Publish_Event()
    {
        // Arrange
        var promotionId = PromotionId.New();
        var discountCode = "OA_10023";
        var percentageDiscount = Percentage.Of(20M);
        var description = "New year discount";
        var buyerId = BuyerId.New();

        // Act
        var promotion = new PromotionBuilder()
            .SetDiscountCode(discountCode)
            .SetPromotionId(promotionId)
            .SetDiscountPercentage(percentageDiscount)
            .SetDescription(description)
            .SetBuyerId(buyerId)
            .Build();

        // Assert
        Assert.Equal(promotionId, promotion.Id);
        Assert.Equal(discountCode, promotion.DiscountCode);
        Assert.Equal(percentageDiscount, promotion.DiscountPercentage);
        Assert.Equal(description, promotion.Description);
        Assert.Equal(buyerId, promotion.BuyerId);
        AssertPublishedDomainEvent<PromotionLaunchedDomainEvent>(promotion);
    }

    [Fact]
    public void ExtendPromotion_Given_Valid_Input_Should_Successfully_Extend_Promotion_And_Publish_Event()
    {
        // Arrange
        var discountCode = "OA_10023";
        const int ExtendedMonths = 2;
        const int ExpirationMonths = 12;
        var date = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(date);

        var promotion = new PromotionBuilder()
            .SetDiscountCode(discountCode)
            .Build();

        // Act
        promotion.Extend(ExtendedMonths);

        // Assert
        Assert.Equal(date.AddMonths(ExtendedMonths + ExpirationMonths), promotion.ExpirationDate);
        AssertPublishedDomainEvent<PromotionExtendedDomainEvent>(promotion);
    }
}
