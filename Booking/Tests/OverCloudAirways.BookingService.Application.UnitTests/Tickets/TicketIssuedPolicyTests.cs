using NSubstitute;
using OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Tickets.Policies.Issued;
using OverCloudAirways.BookingService.TestHelpers.Tickets;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Tickets;

public class TicketIssuedPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelTicketIssuedPolicyHandler_Should_Enqueue_ProjectTicketReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelTicketIssuedPolicyHandler(commandsScheduler);
        var policy = new TicketIssuedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectTicketReadModelCommand>(c => c.TicketId == policy.DomainEvent.TicketId));
    }
}
