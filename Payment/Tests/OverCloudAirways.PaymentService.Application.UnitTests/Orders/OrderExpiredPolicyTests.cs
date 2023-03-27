using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Orders.Policies.Expired;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Orders;

public class OrderExpiredPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelOrderExpiredPolicyHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelOrderExpiredPolicyHandler(commandsScheduler);
        var policy = new OrderExpiredPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectOrderReadModelCommand>(c => c.OrderId == policy.DomainEvent.OrderId));
    }

    [Fact]
    public async Task PublishOrderExpiredPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new OrderExpiredPolicyBuilder().Build();
        var handler = new PublishOrderExpiredPolicyHandler(commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<OrderExpiredIntegrationEvent>(x =>
                x.OrderId == policy.DomainEvent.OrderId));
    }
}
