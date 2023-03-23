using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;
using OverCloudAirways.CrmService.Domain.Promotions.Events;
using OverCloudAirways.CrmService.Domain.UnitTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.CrmService.Domain.UnitTests.Promotions;

public class PromotionTests : Test
{
    [Fact]
    public async Task LaunchPromotion_Given_Valid_Input_Should_Successfully_Launch_Promotion_And_Publish_Event()
    {
        // Arrange
        var promotionId = PromotionId.New();
        var discountCode = "OA_10023";
        var discountCodeGenerator = Substitute.For<IDiscountCodeGenerator>();
        discountCodeGenerator.Generate().Returns(discountCode);
        var percentageDiscount = await Percentage.OfAsync(20M);
        var description = "New year discount";
        var customerId = CustomerId.New();

        // Act
        var promotion = new PromotionBuilder()
            .SetDiscountCodeGenerator(discountCodeGenerator)
            .SetPromotionId(promotionId)
            .SetDiscountPercentage(percentageDiscount)
            .SetDescription(description)
            .SetCustomerId(customerId)
            .Build();

        // Assert
        Assert.Equal(promotionId, promotion.Id);
        Assert.Equal(discountCode, promotion.DiscountCode);
        Assert.Equal(percentageDiscount, promotion.DiscountPercentage);
        Assert.Equal(description, promotion.Description);
        Assert.Equal(customerId, promotion.CustomerId);
        AssertPublishedDomainEvent<PromotionLaunchedDomainEvent>(promotion);
    }

    [Fact]
    public void ExtendPromotion_Given_Valid_Input_Should_Successfully_Extend_Promotion_And_Publish_Event()
    {
        // Arrange
        var discountCode = "OA_10023";
        const int ExtendedMonths = 2;
        const int ExpirationMonths = 12;
        var discountCodeGenerator = Substitute.For<IDiscountCodeGenerator>();
        discountCodeGenerator.Generate().Returns(discountCode);
        var date = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(date);

        var promotion = new PromotionBuilder()
            .SetDiscountCodeGenerator(discountCodeGenerator)
            .Build();

        // Act
        promotion.Extend(ExtendedMonths);

        // Assert
        Assert.Equal(date.AddMonths(ExtendedMonths + ExpirationMonths), promotion.ExpirationDate);
        AssertPublishedDomainEvent<PromotionExtendedDomainEvent>(promotion);
    }
}
