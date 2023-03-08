using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Payments.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Payments;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Payments;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Payments;

[Collection("Payment")]
public class PaymentTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public PaymentTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task ReceivePayment_PaymentShouldBeReceived_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var productId = ProductId.New();
        var buyerId = BuyerId.New();
        var orderId = OrderId.New();
        var invoiceId = InvoiceId.New();
        var paymentId = PaymentId.New();
        var referenceNumber = Guid.NewGuid().ToString();
        var paymentMethod = PaymentMethod.CreditCard;

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

        // Receive Payment
        var receivePaymentCommand = new ReceivePaymentCommandBuilder()
            .SetPaymentId(paymentId)
            .SetInvoiceId(invoiceId)
            .SetAmount(createProductCommand.Price)
            .SetReferenceNumber(referenceNumber)
            .SetMathod(paymentMethod)
            .Build();
        await _invoker.CommandAsync(receivePaymentCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Payment Query
        var query = new GetPaymentInfoQuery(paymentId.Value);
        var payment = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(payment);
        Assert.Equal(paymentId.Value, payment.Id);
        Assert.Equal(referenceNumber, payment.ReferenceNumber);
        Assert.Equal(paymentMethod, payment.Method);
        Assert.Equal(2000M, payment.Amount);
        Assert.Equal(2000M, payment.InvoiceAmount);
    }
}
