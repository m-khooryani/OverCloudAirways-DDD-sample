using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.PaymentService.Domain.Invoices.Rules;

internal class OnlyPendingInvoiceCanBePaidRule : IBusinessRule
{
    private readonly InvoiceStatus _status;

    public OnlyPendingInvoiceCanBePaidRule(InvoiceStatus status)
    {
        _status = status;
    }

    public string TranslationKey => "Only_Pending_Invoice_Can_Be_Paid";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_status == InvoiceStatus.Pending);
    }
}
