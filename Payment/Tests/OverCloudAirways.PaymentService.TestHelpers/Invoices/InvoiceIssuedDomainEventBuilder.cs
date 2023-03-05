using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceIssuedDomainEventBuilder
{
    private InvoiceId _invoiceId = InvoiceId.New();
    private BuyerId _buyerId = BuyerId.New();
    private DateTimeOffset _dueDate = Clock.Now;
    private List<InvoiceItem> _items = new List<InvoiceItem>()
    {
        new InvoiceItemBuilder().Build()
    };

    public InvoiceIssuedDomainEvent Build()
    {
        return new InvoiceIssuedDomainEvent(_invoiceId, _buyerId, _dueDate, _items.AsReadOnly());
    }

    public InvoiceIssuedDomainEventBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public InvoiceIssuedDomainEventBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public InvoiceIssuedDomainEventBuilder SetDueDate(DateTimeOffset dueDate)
    {
        _dueDate = dueDate;
        return this;
    }

    public InvoiceIssuedDomainEventBuilder ClearItems()
    {
        _items.Clear();
        return this;
    }

    public InvoiceIssuedDomainEventBuilder AddToItems(InvoiceItem invoiceItem) 
    {
        _items.Add(invoiceItem);
        return this;
    }
}
