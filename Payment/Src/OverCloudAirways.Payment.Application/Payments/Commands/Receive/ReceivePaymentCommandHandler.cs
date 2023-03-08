using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Payments;

namespace OverCloudAirways.PaymentService.Application.Payments.Commands.Receive;

internal class ReceivePaymentCommandHandler : CommandHandler<ReceivePaymentCommand>
{
    private readonly IAggregateRepository _repository;

    public ReceivePaymentCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ReceivePaymentCommand command, CancellationToken cancellationToken)
    {
        var invoice = await _repository.LoadAsync<Invoice, InvoiceId>(command.InvoiceId);

        var payment = await Payment.ReceiveAsync(
            command.PaymentId,
            invoice,
            command.Amount,
            command.Method,
            command.ReferenceNumber);

        _repository.Add(payment);
    }
}
