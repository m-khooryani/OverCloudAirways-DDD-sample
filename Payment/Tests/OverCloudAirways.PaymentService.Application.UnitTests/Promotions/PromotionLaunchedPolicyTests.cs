using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Promotions.Policies.Launched;
using OverCloudAirways.PaymentService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Promotions;

public class PromotionLaunchedPolicyTests
{
    [Fact]
    public async Task ProjectReadModelPolicyHandler_Should_Enqueue_ProjectPromotionReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new ProjectReadModelPolicyHandler(commandsScheduler);
        var policy = new PromotionLaunchedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectPromotionReadModelCommand>(c => c.PromotionId == policy.DomainEvent.PromotionId));
    }
}
