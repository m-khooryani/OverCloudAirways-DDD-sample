using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Orders.Policies.Failed;
using OverCloudAirways.PaymentService.IntegrationEvents.Orders;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Orders;

public class OrderFailedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelOrderFailedPolicyHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelOrderFailedPolicyHandler(commandsScheduler);
        var policy = new OrderFailedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectOrderReadModelCommand>(c => c.OrderId == policy.DomainEvent.OrderId));
    }

    [Fact]
    public async Task PublishOrderFailedPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new OrderFailedPolicyBuilder().Build();
        var handler = new PublishOrderFailedPolicyHandler(commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<OrderFailedIntegrationEvent>(x =>
                x.OrderId == policy.DomainEvent.OrderId));
    }

    [Fact]
    public async Task RefundBuyerBalanceHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new OrderFailedPolicyBuilder().Build();
        var handler = new RefundBuyerBalanceHandler(commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<RefundBuyerBalanceCommand>(x =>
                x.BuyerId == policy.DomainEvent.BuyerId &&
                x.Amount == policy.DomainEvent.PaidAmount));
    }
}
