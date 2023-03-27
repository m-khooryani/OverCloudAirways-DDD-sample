using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Orders.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Orders;

[Collection("Payment")]
public class OrderTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public OrderTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task PlaceOrder_OrderShouldBePlaced_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var productId = ProductId.New();
        var buyerId = BuyerId.New();
        var orderId = OrderId.New();
        var date = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(date);

        // Register Buyer 
        var registerBuyerCommand = new RegisterBuyerCommandBuilder()
            .SetBuyerId(buyerId)
            .Build();
        await _invoker.CommandAsync(registerBuyerCommand);

        // Create Product 
        var createProductCommand = new CreateProductCommandBuilder()
            .SetProductId(productId)
            .Build();
        await _invoker.CommandAsync(createProductCommand);

        // Place Order
        var placeOrderCommand = new PlaceOrderCommandBuilder()
            .SetOrderId(orderId)
            .SetBuyerId(buyerId)
            .ClearOrderItems()
            .AddOrderItem(new OrderItemBuilder()
                .SetProductId(productId)
                .Build())
            .Build();
        await _invoker.CommandAsync(placeOrderCommand);

        await _testFixture.ProcessOutboxMessagesAsync();

        // Product Query
        var query = new GetOrderInfoQuery(orderId.Value);
        var order = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(orderId.Value, order.Id);
        Assert.Equal(registerBuyerCommand.FirstName, order.BuyerFirstName);
        Assert.Equal(registerBuyerCommand.LastName, order.BuyerLastName);
        Assert.NotEqual(0M, order.TotalAmount);
        Assert.Equal(date, order.Date);
        Assert.Single(order.OrderItems);
        Assert.Equal(createProductCommand.Price, order.OrderItems.Single().ProductPrice);
        Assert.Equal(1, order.OrderItems.Single().Quantity);
        Assert.Equal(createProductCommand.Name, order.OrderItems.Single().ProductName);
        Assert.Equal(createProductCommand.Price * 1, order.OrderItems.Single().TotalPrice);
    }
}
