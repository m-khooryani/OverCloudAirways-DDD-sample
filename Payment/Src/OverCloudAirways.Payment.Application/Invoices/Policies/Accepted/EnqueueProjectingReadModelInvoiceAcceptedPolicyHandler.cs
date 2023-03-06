using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Invoices.Events;

namespace OverCloudAirways.PaymentService.Application.Invoices.Policies.Accepted;

internal class EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler : IDomainPolicyHandler<InvoiceAcceptedPolicy, InvoiceAcceptedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(InvoiceAcceptedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectInvoiceReadModelCommand(notification.DomainEvent.InvoiceId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}