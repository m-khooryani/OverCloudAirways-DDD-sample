using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.Pay;
using OverCloudAirways.PaymentService.Domain.Payments.Events;

namespace OverCloudAirways.PaymentService.Application.Payments.Policies.Received;

public class PayInvoiceHandler : IDomainPolicyHandler<PaymentReceivedPolicy, PaymentReceivedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public PayInvoiceHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PaymentReceivedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new PayInvoiceCommand(notification.DomainEvent.InvoiceId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
