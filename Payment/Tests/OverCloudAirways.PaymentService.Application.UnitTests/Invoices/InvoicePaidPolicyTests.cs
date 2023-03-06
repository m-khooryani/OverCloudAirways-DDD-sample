using NSubstitute;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.PaymentService.Application.Invoices.Commands.ProjectReadModel;
using OverCloudAirways.PaymentService.Application.Invoices.Policies.Paid;
using OverCloudAirways.PaymentService.TestHelpers.Invoices;
using Xunit;

namespace OverCloudAirways.PaymentService.Application.UnitTests.Invoices;

public class InvoicePaidPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelInvoicePaidPolicyHandler_Should_Enqueue_ProjectInvoiceReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelInvoicePaidPolicyHandler(commandsScheduler);
        var policy = new InvoicePaidPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectInvoiceReadModelCommand>(c => c.InvoiceId == policy.DomainEvent.InvoiceId));
    }
}
