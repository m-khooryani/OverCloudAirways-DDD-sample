using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Pay;

internal class PayInvoiceCommandHandler : CommandHandler<PayInvoiceCommand>
{
    private readonly IAggregateRepository _repository;

    public PayInvoiceCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(PayInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = await _repository.LoadAsync<Invoice, InvoiceId>(command.InvoiceId);
        await invoice.PayAsync();
    }
}