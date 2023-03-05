using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Orders;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceBuilder
{
    private IAggregateRepository _repository;
    private InvoiceId _invoiceId = InvoiceId.New();
    private BuyerId _buyerId = BuyerId.New();
    private List<PricedOrderItem> _orderItems = new List<PricedOrderItem>()
    {
        new PricedOrderItemBuilder().Build()
    };

    public async Task<Invoice> BuildAsync()
    {
        return await Invoice.IssueAsync(_repository, _invoiceId, _buyerId, _orderItems);
    }

    public InvoiceBuilder SetAggregateRepository(IAggregateRepository repository)
    {
        _repository = repository;
        return this;
    }

    public InvoiceBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public InvoiceBuilder SetBuyerId(BuyerId buyerId)
    {
        _buyerId = buyerId;
        return this;
    }

    public InvoiceBuilder ClearItems()
    {
        _orderItems.Clear();
        return this;
    }

    public InvoiceBuilder AddToItems(PricedOrderItem item)
    {
        _orderItems.Add(item);
        return this;
    }
}
