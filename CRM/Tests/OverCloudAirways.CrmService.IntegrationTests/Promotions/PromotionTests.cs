using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfo;
using OverCloudAirways.CrmService.Application.Promotions.Queries.GetInfoById;
using OverCloudAirways.CrmService.Domain.Promotions;
using OverCloudAirways.CrmService.IntegrationTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Promotions;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.CrmService.IntegrationTests.Promotions;

[Collection("CRM")]
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
        Clock.SetCustomDate(date);

        // Launch LoyaltyProgram 
        var launchPromotionCommand = new LaunchPromotionCommandBuilder()
            .SetPromotionId(promotionId)
            .SetCustomerId(null)
            .Build();
        await _invoker.CommandAsync(launchPromotionCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Promotion Query ById
        var byIdQuery = new GetPromotionInfoByIdQuery(promotionId.Value);
        var promotion = await _invoker.QueryAsync(byIdQuery);

        // Assert
        Assert.NotNull(promotion);
        Assert.Equal(promotionId.Value, promotion.Id);
        Assert.Equal(launchPromotionCommand.Description, promotion.Description);
        Assert.Equal(launchPromotionCommand.DiscountPercentage, promotion.DiscountPercentage);
        Assert.NotNull(promotion.DiscountCode);
        Assert.Null(promotion.CustomerFirstName);
        Assert.Null(promotion.CustomerLastName);
        Assert.Equal(date.AddYears(1), promotion.ExpirationDate);

        // Promotion Query By DiscountCode
        var query = new GetPromotionInfoQuery(promotion.DiscountCode);
        promotion = await _invoker.QueryAsync(query);

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
