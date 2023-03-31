using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Orders;

public class OrderConfirmedDomainEventBuilder
{
    private OrderId _orderId = OrderId.New();
    private InvoiceId _invoiceId = InvoiceId.New();

    public OrderConfirmedDomainEvent Build()
    {
        return new OrderConfirmedDomainEvent(_orderId, _invoiceId);
    }

    public OrderConfirmedDomainEventBuilder SetOrderId(OrderId orderId)
    {
        _orderId = orderId;
        return this;
    }

    public OrderConfirmedDomainEventBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }
}
