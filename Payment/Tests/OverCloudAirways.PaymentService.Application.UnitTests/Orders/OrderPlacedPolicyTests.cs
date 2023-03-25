using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.PaymentService.Application.Orders.Commands.Expire;
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

    [Fact]
    public async Task ScheduleExpiringOrderPlacedPolicyHandler_Should_Schedule_ExpireOrderCommand()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var expirationDate = now.AddMinutes(15);
        Clock.SetCustomDate(now);
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new ScheduleExpiringOrderPlacedPolicyHandler(commandsScheduler);
        var policy = new OrderPlacedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .ScheduleAsync(
                Arg.Is<ExpireOrderCommand>(c => c.OrderId == policy.DomainEvent.OrderId),
                expirationDate);
    }
}
