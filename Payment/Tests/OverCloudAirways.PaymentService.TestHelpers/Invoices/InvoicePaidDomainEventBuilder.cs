using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoicePaidDomainEventBuilder
{
    private InvoiceId _invoiceId = InvoiceId.New();

    public InvoicePaidDomainEvent Build()
    {
        return new InvoicePaidDomainEvent(_invoiceId);
    }

    public InvoicePaidDomainEventBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }
}
