using NSubstitute;
using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;
using OverCloudAirways.BookingService.IntegrationEvents.Flights;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Flights;

public class FlightSeatsReservedPolicyTests
{
    [Fact]
    public async Task PublishFlightSeatsReservedPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.TcpConnectionId.Returns("test_connection_id");
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new FlightSeatsReservedPolicyBuilder().Build();
        var handler = new PublishFlightSeatsReservedPolicyHandler(userAccessor, commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<FlightSeatsReservedIntegrationEvent>(x =>
                x.FlightId == policy.DomainEvent.FlightId &&
                x.TcpConnectionId == userAccessor.TcpConnectionId &&
                x.SeatsCount == policy.DomainEvent.SeatsCount));
    }

    [Fact]
    public async Task EnqueueProjectingReadModelFlightSeatsReservedPolicyHandler_Should_Enqueue_ProjectFlightReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelFlightSeatsReservedPolicyHandler(commandsScheduler);
        var policy = new FlightSeatsReservedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectFlightReadModelCommand>(c => c.FlightId == policy.DomainEvent.FlightId));
    }
}
