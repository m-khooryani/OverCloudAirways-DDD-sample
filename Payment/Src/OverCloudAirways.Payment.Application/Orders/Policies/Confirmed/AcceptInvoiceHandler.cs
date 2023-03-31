using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.Accept;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Confirmed;

internal class AcceptInvoiceHandler : IDomainPolicyHandler<OrderConfirmedPolicy, OrderConfirmedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public AcceptInvoiceHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderConfirmedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new AcceptInvoiceCommand(notification.DomainEvent.InvoiceId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}