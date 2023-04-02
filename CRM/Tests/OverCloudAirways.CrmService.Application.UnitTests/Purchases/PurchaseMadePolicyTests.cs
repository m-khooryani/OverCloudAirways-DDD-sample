using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.CollectLoyaltyPoints;
using OverCloudAirways.CrmService.Application.Purchases.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Purchases.Policies.Made;
using OverCloudAirways.CrmService.TestHelpers.Purchases;
using Xunit;

namespace OverCloudAirways.CrmService.Application.UnitTests.Purchases;

public class PurchaseMadePolicyTests
{
    [Fact]
    public async Task ProjectReadModelPolicyHandler_Should_Enqueue_ProjectPurchaseReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new ProjectReadModelPolicyHandler(commandsScheduler);
        var policy = new PurchaseMadePolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectPurchaseReadModelCommand>(c => c.PurchaseId == policy.DomainEvent.PurchaseId));
    }

    [Fact]
    public async Task CollectLoyaltyPointsPolicyHandler_Should_Enqueue_ProjectPurchaseReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new CollectLoyaltyPointsPolicyHandler(commandsScheduler);
        var policy = new PurchaseMadePolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<CollectCustomerLoyaltyPointsCommand>(c => 
                c.CustomerId == policy.DomainEvent.CustomerId &&
                c.LoyaltyPoints == policy.DomainEvent.Amount));
    }
}
