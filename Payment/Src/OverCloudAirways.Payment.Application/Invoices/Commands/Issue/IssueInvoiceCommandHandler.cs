using OverCloudAirways.BuildingBlocks.Application.Commands;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders;

namespace OverCloudAirways.PaymentService.Application.Invoices.Commands.Issue;

internal class IssueInvoiceCommandHandler : CommandHandler<IssueInvoiceCommand>
{
    private readonly IAggregateRepository _repository;

    public IssueInvoiceCommandHandler(IAggregateRepository repository)
    {
        _repository = repository;
    }

    public override async Task HandleAsync(IssueInvoiceCommand command, CancellationToken cancellationToken)
    {
        var order = await _repository.LoadAsync<Order, OrderId>(command.OrderId);

        var invoice = await Invoice.IssueAsync(
            _repository,
            command.InvoiceId,
            order.BuyerId,
            order.OrderItems.ToList());

        _repository.Add(invoice);
    }
}