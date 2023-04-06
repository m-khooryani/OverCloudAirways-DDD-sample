using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Promotions.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Promotions;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Promotions;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Promotions;

[Collection("Payment")]
public class PromotionTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public PromotionTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task LaunchPromotion_PromotionShouldBeLaunched_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var promotionId = PromotionId.New();
        var date = DateTimeOffset.UtcNow;
        var discountCode = "OA_10023";
        Clock.SetCustomDate(date);

        // Launch Promotion 
        var launchPromotionCommand = new LaunchPromotionCommandBuilder()
            .SetPromotionId(promotionId)
            .SetBuyerId(null)
            .SetDiscountCode(discountCode)
            .Build();
        await _invoker.CommandAsync(launchPromotionCommand);

        // Process outbox messages
        await _testFixture.ProcessOutboxMessagesAsync();

        // Promotion Query 
        var query = new GetPromotionInfoQuery(discountCode);
        var promotion = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(promotion);
        Assert.Equal(promotionId.Value, promotion.Id);
        Assert.Equal(launchPromotionCommand.Description, promotion.Description);
        Assert.Equal(launchPromotionCommand.DiscountPercentage, promotion.DiscountPercentage);
        Assert.NotNull(promotion.DiscountCode);
        Assert.Null(promotion.CustomerFirstName);
        Assert.Null(promotion.CustomerLastName);
        Assert.Equal(date.AddYears(1), promotion.ExpirationDate);
    }
}
