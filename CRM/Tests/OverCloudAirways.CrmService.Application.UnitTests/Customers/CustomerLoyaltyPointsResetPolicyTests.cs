using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.CrmService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.CrmService.Application.Customers.Policies.LoyaltyPointsReset;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using Xunit;

namespace OverCloudAirways.CrmService.Application.UnitTests.Customers;

public class CustomerLoyaltyPointsResetPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelCustomerLoyaltyPointsResetPolicyHandler_Should_Enqueue_ProjectCustomerReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelCustomerLoyaltyPointsResetPolicyHandler(commandsScheduler);
        var policy = new CustomerLoyaltyPointsResetPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectCustomerReadModelCommand>(c => c.CustomerId == policy.DomainEvent.CustomerId));
    }
}
