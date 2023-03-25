using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Orders;

public class OrderTests : Test
{
    [Fact]
    public async void PlaceOrder_Given_Valid_Input_Should_Successfully_Place_Order_And_Publish_Event()
    {
        // Arrange
        var orderId = OrderId.New();
        var buyerId = BuyerId.New();
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
        var orderItem = new OrderItemBuilder()
            .SetProductId(product.Id)
            .Build();
        var pricedOrderItem = new PricedOrderItemBuilder()
            .SetProductId(product.Id)
            .SetUnitPrice(product.Price)
            .Build();

        var orderBuilder = new OrderBuilder()
            .SetAggregateRepository(repository)
            .SetOrderId(orderId)
            .SetBuyerId(buyerId)
            .ClearItems()
            .AddOrderItem(orderItem);

        // Act
        var order = await orderBuilder.BuildAsync();

        // Assert
        Assert.Equal(orderId, order.Id);
        Assert.Equal(buyerId, order.BuyerId);
        Assert.Single(order.OrderItems);
        Assert.Equal(pricedOrderItem, order.OrderItems.Single());
        Assert.Equal(OrderStatus.Pending, order.Status);
        AssertPublishedDomainEvent<OrderPlacedDomainEvent>(order);
    }
}
