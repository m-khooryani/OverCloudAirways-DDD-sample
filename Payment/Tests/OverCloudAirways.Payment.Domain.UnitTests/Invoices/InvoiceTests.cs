using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Invoices;

public class InvoiceTests : Test
{
    [Fact]
    public async void IssueInvoice_Given_Valid_Input_Should_Successfully_Issue_Invoice_And_Publish_Event()
    {
        // Arrange
        var invoiceId = InvoiceId.New();
        var now = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(now);
        var buyerId = BuyerId.New();
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);
        var pricedOrderItem = new PricedOrderItemBuilder()
            .SetProductId(product.Id)
            .SetUnitPrice(product.Price)
            .Build();

        var invoiceBuilder = new InvoiceBuilder()
            .SetInvoiceId(invoiceId)
            .SetBuyerId(buyerId)
            .ClearItems()
            .AddToItems(pricedOrderItem)
            .SetAggregateRepository(repository);

        // Act
        var invoice = await invoiceBuilder.BuildAsync();

        // Assert
        Assert.Equal(invoiceId, invoice.Id);
        Assert.Equal(buyerId, invoice.BuyerId);
        Assert.Equal(InvoiceStatus.Pending, invoice.Status);
        Assert.Equal(now, invoice.DueDate);
        Assert.Single(invoice.Items);
        AssertPublishedDomainEvent<InvoiceIssuedDomainEvent>(invoice);
    }
}
