using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Products.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Products.Policies.Created;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Products;

public class ProductCreatedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelProductCreatedPolicyHandler_Should_Enqueue_ProjectProductReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelProductCreatedPolicyHandler(commandsScheduler);
        var policy = new ProductCreatedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectProductReadModelCommand>(c => c.ProductId == policy.DomainEvent.ProductId));
    }
}
