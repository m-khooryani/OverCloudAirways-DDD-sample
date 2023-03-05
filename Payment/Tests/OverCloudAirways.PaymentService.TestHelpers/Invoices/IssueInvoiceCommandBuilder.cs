using OverCloudAirways.PaymentService.Application.Invoices.Commands.Issue;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class IssueInvoiceCommandBuilder
{
    private InvoiceId _invoiceId = InvoiceId.New();
    private OrderId _orderId = OrderId.New();

    public IssueInvoiceCommand Build()
    {
        return new IssueInvoiceCommand(_invoiceId, _orderId);
    }

    public IssueInvoiceCommandBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public IssueInvoiceCommandBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }
}
