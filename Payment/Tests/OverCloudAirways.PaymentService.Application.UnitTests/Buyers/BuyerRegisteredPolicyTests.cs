using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Buyers.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Buyers.Policies.Registered;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Buyers;

public class BuyerRegisteredPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelBuyerCreatedPolicyHandler_Should_Enqueue_ProjectBuyerReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelBuyerCreatedPolicyHandler(commandsScheduler);
        var policy = new BuyerRegisteredPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectBuyerReadModelCommand>(c => c.BuyerId == policy.DomainEvent.BuyerId));
    }
}
