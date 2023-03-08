using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Payments;
using OverCloudAirways.PaymentService.Domain.Payments.Events;
using OverCloudAirways.PaymentService.Domain.Payments.Rules;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using OverCloudAirways.PaymentService.TestHelpers.Payments;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Payments;

public class PaymentTests : Test
{
    [Fact]
    public async Task ReceivePayment_Given_NotPendingInvoice_Should_Throw_Business_Error()
    {
        // Arrange
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);

        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .SetAggregateRepository(repository)
            .BuildAsync();
        await invoice.PayAsync();
        var paymentBuilder = new PaymentBuilder()
            .SetInvoice(invoice);

        // Act, Assert
        await AssertViolatedRuleAsync<PaymentCanOnlyBeMadeForPendingInvoiceRule>(async () =>
        {
            _ = await paymentBuilder.BuildAsync();
        });
    }

    [Fact]
    public async Task ReceivePayment_Given_Wrong_TotalAmount_Should_Throw_Business_Error()
    {
        // Arrange
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);

        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .SetAggregateRepository(repository)
            .BuildAsync();
        var paymentBuilder = new PaymentBuilder()
            .SetAmount(invoice.TotalAmount + 10M)
            .SetInvoice(invoice);

        // Act, Assert
        await AssertViolatedRuleAsync<PaymentAmountShouldMatchTheTotalAmountOfTheInvoiceItIsAssociatedWithRule>(async () =>
        {
            _ = await paymentBuilder.BuildAsync();
        });
    }

    [Fact]
    public async void ReceivePayment_Given_Valid_Input_Should_Successfully_Receive_Payment_And_Publish_Event()
    {
        // Arrange
        var paymentId = PaymentId.New();
        var referenceNumber = Guid.NewGuid().ToString();
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);

        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .SetAggregateRepository(repository)
            .BuildAsync();
        var payment = await new PaymentBuilder()
            .SetPaymentId(paymentId)
            .SetReferenceNumber(referenceNumber)
            .SetAmount(invoice.TotalAmount)
            .SetInvoice(invoice)
            .BuildAsync();

        // Assert
        AssertPublishedDomainEvent<PaymentReceivedDomainEvent>(payment);
        Assert.Equal(paymentId, payment.Id);
        Assert.Equal(referenceNumber, payment.ReferenceNumber);
        Assert.Equal(invoice.TotalAmount, payment.Amount);
    }
}
