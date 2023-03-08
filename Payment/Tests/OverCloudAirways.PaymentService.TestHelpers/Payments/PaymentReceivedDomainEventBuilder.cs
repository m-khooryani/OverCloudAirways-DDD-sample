using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;
using OverCloudAirways.PaymentService.Domain.Payments.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Payments;

public class PaymentReceivedDomainEventBuilder
{
    private PaymentId _paymentId = PaymentId.New();
    private decimal _amount = 100M;
    private InvoiceId _invoiceId = InvoiceId.New();
    private PaymentMethod _method = PaymentMethod.CreditCard;
    private string _referenceNumber = Guid.NewGuid().ToString();

    public PaymentReceivedDomainEvent Build()
    {
        return new PaymentReceivedDomainEvent(_paymentId, _amount, _invoiceId, _method, _referenceNumber);
    }

    public PaymentReceivedDomainEventBuilder SetPaymentId(PaymentId paymentId)
    {
        _paymentId = paymentId;
        return this;
    }

    public PaymentReceivedDomainEventBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public PaymentReceivedDomainEventBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public PaymentReceivedDomainEventBuilder SetMathod(PaymentMethod method)
    {
        _method = method;
        return this;
    }

    public PaymentReceivedDomainEventBuilder SetReferenceNumber(string referenceNumber)
    {
        _referenceNumber = referenceNumber;
        return this;
    }
}
