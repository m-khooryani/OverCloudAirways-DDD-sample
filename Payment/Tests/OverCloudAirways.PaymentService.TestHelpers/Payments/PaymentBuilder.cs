using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.TestHelpers.Payments;

public class PaymentBuilder
{
    private PaymentId _paymentId = PaymentId.New();
    private decimal _amount = 100M;
    private Invoice _invoice = null;
    private PaymentMethod _method = PaymentMethod.CreditCard;
    private string _referenceNumber = Guid.NewGuid().ToString();

    public async Task<Payment> BuildAsync()
    {
        return await Payment.ReceiveAsync(_paymentId, _invoice, _amount, _method, _referenceNumber);
    }

    public PaymentBuilder SetPaymentId(PaymentId paymentId)
    {
        _paymentId = paymentId;
        return this;
    }

    public PaymentBuilder SetAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public PaymentBuilder SetInvoice(Invoice invoice)
    {
        _invoice = invoice;
        return this;
    }

    public PaymentBuilder SetMathod(PaymentMethod method)
    {
        _method = method;
        return this;
    }

    public PaymentBuilder SetReferenceNumber(string referenceNumber)
    {
        _referenceNumber = referenceNumber;
        return this;
    }
}
