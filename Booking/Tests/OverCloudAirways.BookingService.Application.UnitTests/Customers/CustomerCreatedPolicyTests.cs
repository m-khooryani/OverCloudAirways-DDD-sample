using NSubstitute;
using OverCloudAirways.BookingService.Application.Customers.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Customers.Policies.Created;
using OverCloudAirways.BookingService.TestHelpers.Customers;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Customers;

public class CustomerCreatedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelCustomerCreatedPolicyHandler_Should_Enqueue_ProjectCustomerReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelCustomerCreatedPolicyHandler(commandsScheduler);
        var policy = new CustomerCreatedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectCustomerReadModelCommand>(c => c.CustomerId == policy.DomainEvent.CustomerId));
    }
}
