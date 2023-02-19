using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Products.Policies.Disabled;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Products;

public class ProductDisabledPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelProductDisabledPolicyHandler_Should_Enqueue_ProjectProductReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelProductDisabledPolicyHandler(commandsScheduler);
        var policy = new ProductDisabledPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectProductReadModelCommand>(c => c.ProductId == policy.DomainEvent.ProductId));
    }
}
