using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Promotions.Policies.Launched;
using OverCloudAirways.CrmService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.CrmService.Application.UnitTests.Promotions;

public class PromotionLaunchedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelPromotionLaunchedPolicyHandler_Should_Enqueue_ProjectPromotionReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelPromotionLaunchedPolicyHandler(commandsScheduler);
        var policy = new PromotionLaunchedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectPromotionReadModelCommand>(c => c.PromotionId == policy.DomainEvent.PromotionId));
    }
}
