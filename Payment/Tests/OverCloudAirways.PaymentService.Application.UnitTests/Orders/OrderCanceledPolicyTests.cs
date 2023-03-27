using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Orders.Policies.Canceled;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Orders;

public class OrderCanceledPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelOrderCanceledPolicyHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelOrderCanceledPolicyHandler(commandsScheduler);
        var policy = new OrderCanceledPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectOrderReadModelCommand>(c => c.OrderId == policy.DomainEvent.OrderId));
    }

    [Fact]
    public async Task PublishOrderCanceledPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new OrderCanceledPolicyBuilder().Build();
        var handler = new PublishOrderCanceledPolicyHandler(commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<OrderCanceledIntegrationEvent>(x =>
                x.OrderId == policy.DomainEvent.OrderId));
    }
}
