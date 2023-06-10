using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.Issue;
using OverCloudAirways.PaymentService.Domain.Invoices;
using OverCloudAirways.PaymentService.Domain.Orders.Events;

namespace OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;

internal class EnqueueIssuingInvoicePolicyHandler : IDomainPolicyHandler<OrderPlacedPolicy, OrderPlacedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueIssuingInvoicePolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(OrderPlacedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new IssueInvoiceCommand(InvoiceId.New(), notification.DomainEvent.OrderId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
