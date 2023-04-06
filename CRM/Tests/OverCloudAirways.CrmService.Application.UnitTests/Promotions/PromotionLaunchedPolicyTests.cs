using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Promotions.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Promotions.Policies.Launched;
using OverCloudAirways.CrmService.IntegrationEvents.Promotions;
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

    [Fact]
    public async Task PublishIntegrationEventPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new PublishIntegrationEventPolicyHandler(commandsScheduler);
        var policy = new PromotionLaunchedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<PromotionLaunchedIntegrationEvent>(x =>
                x.PromotionId == policy.DomainEvent.PromotionId &&
                x.CustomerId == policy.DomainEvent.CustomerId &&
                x.Description == policy.DomainEvent.Description &&
                x.DiscountCode == policy.DomainEvent.DiscountCode &&
                x.DiscountPercentage == policy.DomainEvent.DiscountPercentage &&
                x.ExpirationDate == policy.DomainEvent.ExpirationDate));
    }
}
