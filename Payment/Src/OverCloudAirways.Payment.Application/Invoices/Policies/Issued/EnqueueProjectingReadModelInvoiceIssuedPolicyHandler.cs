using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Issued;

internal class EnqueueProjectingReadModelInvoiceIssuedPolicyHandler : IDomainPolicyHandler<InvoiceIssuedPolicy, InvoiceIssuedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelInvoiceIssuedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(InvoiceIssuedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectInvoiceReadModelCommand(notification.DomainEvent.InvoiceId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}