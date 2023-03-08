using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Domain.Payments.Rules;

internal class PaymentCanOnlyBeMadeForPendingInvoiceRule : IBusinessRule
{
    private readonly Invoice _invoice;

    public PaymentCanOnlyBeMadeForPendingInvoiceRule(Invoice invoice)
    {
        _invoice = invoice;
    }

    public string TranslationKey => "Payment_Can_Only_Be_Made_For_Pending_Invoice";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_invoice.Status == InvoiceStatus.Pending);
    }
}
