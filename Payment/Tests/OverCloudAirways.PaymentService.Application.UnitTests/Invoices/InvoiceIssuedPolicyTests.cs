using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Invoices.Policies.Issued;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Invoices;

public class InvoiceIssuedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelInvoiceIssuedPolicyHandler_Should_Enqueue_ProjectInvoiceReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelInvoiceIssuedPolicyHandler(commandsScheduler);
        var policy = new InvoiceIssuedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectInvoiceReadModelCommand>(c => c.InvoiceId == policy.DomainEvent.InvoiceId));
    }
}
