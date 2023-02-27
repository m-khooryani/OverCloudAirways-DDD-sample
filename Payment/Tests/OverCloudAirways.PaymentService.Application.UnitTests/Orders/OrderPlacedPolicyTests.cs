using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Orders.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Orders.Policies.Placed;
using OverCloudAirways.PaymentService.TestHelpers.Orders;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Orders;

public class OrderPlacedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelOrderPlacedPolicyHandler_Should_Enqueue_ProjectOrderReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelOrderPlacedPolicyHandler(commandsScheduler);
        var policy = new OrderPlacedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectOrderReadModelCommand>(c => c.OrderId == policy.DomainEvent.OrderId));
    }
}
