using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Paid;

internal class EnqueueProjectingReadModelInvoicePaidPolicyHandler : IDomainPolicyHandler<InvoicePaidPolicy, InvoicePaidDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelInvoicePaidPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(InvoicePaidPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectInvoiceReadModelCommand(notification.DomainEvent.InvoiceId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}