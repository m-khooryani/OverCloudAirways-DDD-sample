using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Orders.Commands.Confirm;

internal class ConfirmOrderCommandHandler : CommandHandler<ConfirmOrderCommand>
{
    private readonly IAggregateRepository _repository;

    public ConfirmOrderCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(ConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.LoadAsync<Order, OrderId>(command.OrderId);
        var invoice = await _repository.LoadAsync<Invoice, InvoiceId>(command.InvoiceId);

        order.Confirm(invoice);
    }
}