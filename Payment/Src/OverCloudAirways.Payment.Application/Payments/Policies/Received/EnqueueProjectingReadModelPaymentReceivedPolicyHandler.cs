using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Domain.Payments.Events;

namespace OverCloudAirways.PaymentService.Application.Payments.Policies.Received;

public class EnqueueProjectingReadModelPaymentReceivedPolicyHandler 
    : IDomainPolicyHandler<PaymentReceivedPolicy, PaymentReceivedDomainEvent>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public EnqueueProjectingReadModelPaymentReceivedPolicyHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public async Task Handle(PaymentReceivedPolicy notification, CancellationToken cancellationToken)
    {
        var command = new ProjectPaymentReadModelCommand(notification.DomainEvent.PaymentId);
        await _commandsScheduler.EnqueueAsync(command);
    }
}
