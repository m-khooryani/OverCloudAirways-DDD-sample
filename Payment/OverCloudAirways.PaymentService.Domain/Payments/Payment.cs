using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments.Events;
using OverCloudAirways.PaymentService.Domain.Payments.Rules;

namespace OverCloudAirways.PaymentService.Domain.Payments;

public class Payment : AggregateRoot<PaymentId>
{
    public decimal Amount { get; private set; }
    public InvoiceId InvoiceId { get; private set; }
    public PaymentMethod Method { get; private set; }
    public string ReferenceNumber { get; private set; }

    private Payment()
    {
    }

    public static async Task<Payment> ReceiveAsync(
        PaymentId paymentId,
        Invoice invoice,
        decimal amount,
        PaymentMethod method,
        string referenceNumber)
    {
        await CheckRuleAsync(new PaymentCanOnlyBeMadeForPendingInvoiceRule(invoice));
        await CheckRuleAsync(new PaymentAmountShouldMatchTheTotalAmountOfTheInvoiceItIsAssociatedWithRule(invoice, amount));

        var payment = new Payment();
        var @event = new PaymentReceivedDomainEvent(paymentId, amount, invoice.Id, method, referenceNumber);
        payment.Apply(@event);

        return payment;
    }

    protected void When(PaymentReceivedDomainEvent @event)
    {
        Id = @event.PaymentId;
        Amount = @event.Amount;
        InvoiceId = @event.InvoiceId;
        Method = @event.Method;
        ReferenceNumber = @event.ReferenceNumber;
    }
}
