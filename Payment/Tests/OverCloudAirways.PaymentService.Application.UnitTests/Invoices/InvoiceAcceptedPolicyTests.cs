using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Invoices.Policies.Accepted;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Invoices;

public class InvoiceAcceptedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler_Should_Enqueue_ProjectInvoiceReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelInvoiceAcceptedPolicyHandler(commandsScheduler);
        var policy = new InvoiceAcceptedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectInvoiceReadModelCommand>(c => c.InvoiceId == policy.DomainEvent.InvoiceId));
    }
}
