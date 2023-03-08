using OverCloudAirways.PaymentService.Application.Payments.Commands.Receive;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.TestHelpers.Payments;

public class ReceivePaymentCommandBuilder
{
    private PaymentId _paymentId = PaymentId.New();
    private decimal _amount = 100M;
    private InvoiceId _invoice = InvoiceId.New();
    private PaymentMethod _method = PaymentMethod.CreditCard;
    private string _referenceNumber = Guid.NewGuid().ToString();

    public ReceivePaymentCommand Build()
    {
        return new ReceivePaymentCommand(_paymentId, _invoice, _amount, _method, _referenceNumber);
    }

    public ReceivePaymentCommandBuilder SetPaymentId(PaymentId paymentId)
    {
        _paymentId = paymentId;
        return this;
    }

    public ReceivePaymentCommandBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public ReceivePaymentCommandBuilder SetInvoiceId(InvoiceId invoiceId)
    {
        _invoice = invoiceId;
        return this;
    }

    public ReceivePaymentCommandBuilder SetMathod(PaymentMethod method)
    {
        _method = method;
        return this;
    }

    public ReceivePaymentCommandBuilder SetReferenceNumber(string referenceNumber)
    {
        _referenceNumber = referenceNumber;
        return this;
    }
}
