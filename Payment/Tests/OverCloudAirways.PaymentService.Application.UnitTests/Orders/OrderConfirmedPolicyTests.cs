using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Orders.Policies.Confirmed;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Orders;

public class OrderConfirmedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelOrderConfirmedPolicyHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelOrderConfirmedPolicyHandler(commandsScheduler);
        var policy = new OrderConfirmedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectOrderReadModelCommand>(c => c.OrderId == policy.DomainEvent.OrderId));
    }
}
