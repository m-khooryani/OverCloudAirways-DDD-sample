using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Payments.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Payments.Policies.Received;
using OverCloudAirways.PaymentService.TestHelpers.Payments;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Payments;

public class PaymentReceivedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelPaymentReceivedPolicyHandler_Should_Enqueue_ProjectPaymentReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelPaymentReceivedPolicyHandler(commandsScheduler);
        var policy = new PaymentReceivedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectPaymentReadModelCommand>(c => c.PaymentId == policy.DomainEvent.PaymentId));
    }
}
