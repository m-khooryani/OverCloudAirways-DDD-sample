using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Invoices.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Invoices;

[Collection("Payment")]
public class InvoiceTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public InvoiceTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task IssueInvoice_InvoiceShouldBeIssued_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var productId = ProductId.New();
        var buyerId = BuyerId.New();
        var orderId = OrderId.New();
        var invoiceId = InvoiceId.New();
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

        // Issue Invoice
        var issueInvoiceCommand = new IssueInvoiceCommandBuilder()
            .SetInvoiceId(invoiceId)
            .SetOrderId(orderId)
            .Build();
        await _invoker.CommandAsync(issueInvoiceCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Product Query
        var query = new GetInvoiceInfoQuery(invoiceId.Value);
        var invoice = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal(invoiceId.Value, invoice.Id);
        Assert.Equal(registerBuyerCommand.FirstName, invoice.BuyerFirstName);
        Assert.Equal(registerBuyerCommand.LastName, invoice.BuyerLastName);
        Assert.Single(invoice.Items);
        Assert.Equal(createProductCommand.Price, invoice.Items.Single().UnitPrice);
        Assert.Equal(1, invoice.Items.Single().Quantity);
        Assert.Equal(createProductCommand.Name, invoice.Items.Single().Description);
        Assert.Equal(createProductCommand.Price * 1, invoice.Items.Single().TotalPrice);
    }
}
