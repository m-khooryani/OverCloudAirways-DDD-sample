using FluentValidation;
using OverCloudAirways.BuildingBlocks.Application.Validators;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Issue;

internal class IssueInvoiceCommandValidator : CommandValidator<IssueInvoiceCommand>
{
    public IssueInvoiceCommandValidator()
    {
        RuleFor(x => x.InvoiceId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
    }
}