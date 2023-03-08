using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Domain.Payments.Rules;

internal class PaymentAmountShouldMatchTheTotalAmountOfTheInvoiceItIsAssociatedWithRule : IBusinessRule
{
    private readonly Invoice _invoice;
    private readonly decimal _amount;

    public PaymentAmountShouldMatchTheTotalAmountOfTheInvoiceItIsAssociatedWithRule(
        Invoice invoice, 
        decimal amount)
    {
        _invoice = invoice;
        _amount = amount;
    }

    public string TranslationKey => "Payment_Amount_Should_Match_The_Total_Amount_Of_The_Invoice_It_Is_Associated_With";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(decimal.Equals(_invoice.TotalAmount, _amount));
    }
}
