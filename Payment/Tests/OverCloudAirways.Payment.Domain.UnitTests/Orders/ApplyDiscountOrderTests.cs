using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Orders.Rules;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Promotions;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using OverCloudAirways.PaymentService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Orders;

public class ApplyDiscountOrderTests : Test
{
    [Fact]
    public async void ApplyDiscount_Given_Valid_Input_Should_Successfully_Cancel_Order_And_Publish_Event()
    {
        // Arrange
        var price = 200M;
        var buyerId = BuyerId.New();
        var discountPercentage = Percentage.Of(20);
        var product = new ProductBuilder()
            .SetPrice(price)
            .Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
        var orderItem = new OrderItemBuilder()
            .SetProductId(product.Id)
            .Build();
        var order = await new OrderBuilder()
            .SetAggregateRepository(repository)
            .ClearItems()
            .AddOrderItem(orderItem)
            .SetBuyerId(buyerId)
            .BuildAsync();

        var promotion = new PromotionBuilder()
            .SetBuyerId(buyerId)
            .SetDiscountPercentage(discountPercentage)
            .Build();

        // Act
        await order.ApplyDiscountAsync(promotion);

        // Assert
        Assert.Equal(price * ((100M - discountPercentage) / 100), order.TotalAmount);
        AssertPublishedDomainEvent<DiscountAppliedToOrderDomainEvent>(order);
    }

    [Fact]
    public async void ApplyDiscount_Given_Invalid_Buyer_Should_Throw_Business_Error()
    {
        // Arrange
        var price = 200M;
        var discountPercentage = Percentage.Of(20);
        var product = new ProductBuilder()
            .SetPrice(price)
            .Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
        var orderItem = new OrderItemBuilder()
            .SetProductId(product.Id)
            .Build();
        var order = await new OrderBuilder()
            .SetAggregateRepository(repository)
            .ClearItems()
            .AddOrderItem(orderItem)
            .BuildAsync();

        var promotion = new PromotionBuilder()
            .SetDiscountPercentage(discountPercentage)
            .Build();

        // Act, Assert
        await AssertViolatedRuleAsync<DiscountCanBeAppliedForOrderWithValidBuyerRule>(async () =>
        {
            await order.ApplyDiscountAsync(promotion);
        });
    }

    [Fact]
    public async void ApplyDiscount_Given_Expired_Promotion_Should_Throw_Business_Error()
    {
        // Arrange
        var price = 200M;
        var buyerId = BuyerId.New();
        var discountPercentage = Percentage.Of(20);
        var product = new ProductBuilder()
            .SetPrice(price)
            .Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
        var orderItem = new OrderItemBuilder()
            .SetProductId(product.Id)
            .Build();

        var order = await new OrderBuilder()
            .SetAggregateRepository(repository)
            .ClearItems()
            .AddOrderItem(orderItem)
            .SetBuyerId(buyerId)
            .BuildAsync();

        var promotion = new PromotionBuilder()
            .SetBuyerId(buyerId)
            .SetDiscountPercentage(discountPercentage)
            .Build();

        Clock.SetCustomDate(DateTime.UtcNow.AddDays(9999));

        // Act, Assert
        await AssertViolatedRuleAsync<DiscountCanBeAppliedBeforeExpirationRule>(async () =>
        {
            await order.ApplyDiscountAsync(promotion);
        });
    }
}
