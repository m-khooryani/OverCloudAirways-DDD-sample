using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.PaymentService.Domain.Invoices.Rules;

internal class OnlyPaidInvoiceCanBeAcceptedRule : IBusinessRule
{
    private readonly InvoiceStatus _status;

    public OnlyPaidInvoiceCanBeAcceptedRule(InvoiceStatus status)
    {
        _status = status;
    }

    public string TranslationKey => "Only_Paid_Invoice_Can_Be_Accepted";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_status == InvoiceStatus.Paid);
    }
}
