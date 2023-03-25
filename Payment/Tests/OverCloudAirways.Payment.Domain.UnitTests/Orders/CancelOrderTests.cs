using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Orders.Rules;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Orders;

public class CancelOrderTests : Test
{
    [Fact]
    public async void CancelOrder_Given_Valid_Input_Should_Successfully_Cancel_Order_And_Publish_Event()
    {
        // Arrange
        var product = new ProductBuilder().Build();
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

        // Act
        await order.CancelAsync();

        // Assert
        Assert.Equal(OrderStatus.Canceled, order.Status);
        AssertPublishedDomainEvent<OrderCanceledDomainEvent>(order);
    }

    [Fact]
    public async void CancelOrder_Given_NonPending_Order_Should_Throw_Business_Error()
    {
        // Arrange
        var product = new ProductBuilder().Build();
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

        await order.ExpireAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyPendingOrdersCanBeModifiedRule>(async () =>
        {
            await order.CancelAsync();
        });
    }
}
