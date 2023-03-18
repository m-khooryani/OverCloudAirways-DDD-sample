using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsCollected;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using Xunit;

namespace OverCloudAirways.CrmService.Application.UnitTests.Customers;

public class CustomerLoyaltyPointsCollectedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelCustomerLoyaltyPointsCollectedPolicyHandler_Should_Enqueue_ProjectCustomerReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelCustomerLoyaltyPointsCollectedPolicyHandler(commandsScheduler);
        var policy = new CustomerLoyaltyPointsCollectedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectCustomerReadModelCommand>(c => c.CustomerId == policy.DomainEvent.CustomerId));
    }
}
