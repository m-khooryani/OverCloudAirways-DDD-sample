using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Orders;

public class ConfirmOrderTests : Test
{
    [Fact]
    public async void ConfirmOrder_Given_Invoice_Equal_TotalAmount_Input_Should_Successfully_Confirm_Order_And_Publish_Event()
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

        var pricedOrderItem = new PricedOrderItemBuilder()
            .SetProductId(product.Id)
            .SetUnitPrice(product.Price)
            .Build();
        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .AddToItems(pricedOrderItem)
            .SetAggregateRepository(repository)
            .BuildAsync();

        // Act
        order.Confirm(invoice);

        // Assert
        Assert.Equal(OrderStatus.Confirmed, order.Status);
        AssertPublishedDomainEvent<OrderConfirmedDomainEvent>(order);
    }

    [Fact]
    public async void ConfirmOrder_Given_Invoice_NotEqual_TotalAmount_Input_Should_Fail_Order_And_Publish_Event()
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

        // Generate Invoice with different price
        product.Update(product.Name, product.Description, product.Price - 1M);

        var pricedOrderItem = new PricedOrderItemBuilder()
            .SetProductId(product.Id)
            .SetUnitPrice(product.Price)
            .Build();
        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .AddToItems(pricedOrderItem)
            .SetAggregateRepository(repository)
            .BuildAsync();

        // Act
        order.Confirm(invoice);

        // Assert
        Assert.Equal(OrderStatus.Failed, order.Status);
        AssertPublishedDomainEvent<OrderFailedDomainEvent>(order);
    }
}
