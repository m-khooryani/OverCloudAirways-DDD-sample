﻿using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;
using OverCloudAirways.PaymentService.Domain.Invoices.Rules;
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

    [Fact]
    public async void PayInvoice_Given_Valid_Input_Should_Successfully_Pay_Invoice_And_Publish_Event()
    {
        // Arrange
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);

        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .SetAggregateRepository(repository)
            .BuildAsync();

        // Act
        await invoice.PayAsync();

        // Assert
        Assert.Equal(InvoiceStatus.Paid, invoice.Status);
        AssertPublishedDomainEvent<InvoicePaidDomainEvent>(invoice);
    }

    [Fact]
    public async Task PayInvoice_Given_NotPendingInvoice_Should_Throw_Business_Error()
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
        await invoice.AcceptAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyPendingInvoiceCanBePaidRule>(async () =>
        {
            await invoice.PayAsync();
        });
    }

    [Fact]
    public async void AcceptInvoice_Given_Valid_Input_Should_Successfully_Accept_Invoice_And_Publish_Event()
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

        // Act
        await invoice.AcceptAsync();

        // Assert
        Assert.Equal(InvoiceStatus.Accepted, invoice.Status);
        AssertPublishedDomainEvent<InvoiceAcceptedDomainEvent>(invoice);
    }

    [Fact]
    public async Task AcceptInvoice_Given_NotPaidInvoice_Should_Throw_Business_Error()
    {
        // Arrange
        var product = new ProductBuilder().Build();
        var repository = Substitute.For<IAggregateRepository>();
        repository.LoadAsync<Product, ProductId>(product.Id).Returns(product);

        var invoice = await new InvoiceBuilder()
            .ClearItems()
            .SetAggregateRepository(repository)
            .BuildAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyPaidInvoiceCanBeAcceptedRule>(async () =>
        {
            await invoice.AcceptAsync();
        });
    }
}
