using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Accept;

internal class AcceptInvoiceCommandHandler : CommandHandler<AcceptInvoiceCommand>
{
    private readonly IAggregateRepository _repository;

    public AcceptInvoiceCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(AcceptInvoiceCommand command, CancellationToken cancellationToken)
    {
        var invoice = await _repository.LoadAsync<Invoice, InvoiceId>(command.InvoiceId);
        await invoice.AcceptAsync();
    }
}