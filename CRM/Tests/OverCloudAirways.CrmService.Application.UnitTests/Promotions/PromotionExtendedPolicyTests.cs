using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Promotions.Policies.Extended;
using OverCloudAirways.CrmService.IntegrationEvents.Promotions;
using OverCloudAirways.CrmService.TestHelpers.Promotions;
using Xunit;

namespace OverCloudAirways.CrmService.Application.UnitTests.Promotions;

public class PromotionExtendedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelPromotionExtendedPolicyHandler_Should_Enqueue_ProjectPromotionReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelPromotionExtendedPolicyHandler(commandsScheduler);
        var policy = new PromotionExtendedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectPromotionReadModelCommand>(c => c.PromotionId == policy.DomainEvent.PromotionId));
    }

    [Fact]
    public async Task PublishIntegrationEventPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new PublishIntegrationEventPolicyHandler(commandsScheduler);
        var policy = new PromotionExtendedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<PromotionExtendedIntegrationEvent>(x =>
                x.PromotionId == policy.DomainEvent.PromotionId &&
                x.Months == policy.DomainEvent.Months));
    }
}
