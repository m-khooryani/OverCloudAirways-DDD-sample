using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceAcceptedDomainEventBuilder
{
    private InvoiceId _invoiceId = InvoiceId.New();

    public InvoiceAcceptedDomainEvent Build()
    {
        return new InvoiceAcceptedDomainEvent(_invoiceId);
    }

    public InvoiceAcceptedDomainEventBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }
}
