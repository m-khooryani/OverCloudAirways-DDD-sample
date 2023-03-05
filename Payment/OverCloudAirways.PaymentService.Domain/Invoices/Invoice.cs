using System.Collections.ObjectModel;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Domain.Invoices;

public class Invoice : AggregateRoot<InvoiceId>
{
    public BuyerId BuyerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTimeOffset DueDate { get; private set; }
    public InvoiceStatus Status { get; private set; }
    private List<InvoiceItem> _items;
    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

    private Invoice()
    {
    }

    public static async Task<Invoice> IssueAsync(
        IAggregateRepository repository,
        InvoiceId invoiceId,
        BuyerId buyerId,
        IReadOnlyList<PricedOrderItem> orderItems)
    {
        var pricedOrderItems = await GetInvoiceItems(repository, orderItems);

        var invoice = new Invoice();
        var @event = new InvoiceIssuedDomainEvent(
            invoiceId,
            buyerId,
            Clock.Now,
            pricedOrderItems);
        invoice.Apply(@event);

        return invoice;
    }

    private static async Task<ReadOnlyCollection<InvoiceItem>> GetInvoiceItems(
        IAggregateRepository repository,
        IReadOnlyList<PricedOrderItem> orderItems)
    {
        var invoiceItems = new List<InvoiceItem>();
        foreach (var orderItem in orderItems)
        {
            var product = await repository.LoadAsync<Product, ProductId>(orderItem.ProductId);
            invoiceItems.Add(InvoiceItem.Of(orderItem, product));
        }

        return invoiceItems.AsReadOnly();
    }

    protected void When(InvoiceIssuedDomainEvent @event)
    {
        Id = @event.InvoiceId;
        BuyerId = @event.BuyerId;
        TotalAmount = @event.InvoiceItems.Sum(oi => oi.TotalPrice);
        DueDate = @event.DueDate;
        Status = InvoiceStatus.Pending;
        _items = new (@event.InvoiceItems);
    }
}
