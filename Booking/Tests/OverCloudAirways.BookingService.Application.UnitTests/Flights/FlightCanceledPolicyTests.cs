using NSubstitute;
using OverCloudAirways.BookingService.Application.Flights.Commands.ProjectReadModel;
using OverCloudAirways.BookingService.Application.Flights.Policies.Canceled;
using OverCloudAirways.BookingService.IntegrationEvents.Flights;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.Flights;

public class FlightCanceledPolicyTests
{
    [Fact]
    public async Task EnqueueProjectingReadModelFlightCanceledPolicyHandler_Should_Enqueue_ProjectFlightReadModelCommand()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueProjectingReadModelFlightCanceledPolicyHandler(commandsScheduler);
        var policy = new FlightCanceledPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ProjectFlightReadModelCommand>(c => c.FlightId == policy.DomainEvent.FlightId));
    }

    [Fact]
    public async Task PublishFlightCanceledPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var policy = new FlightCanceledPolicyBuilder().Build();
        var handler = new PublishFlightCanceledPolicyHandler(commandsScheduler);

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<FlightCanceledIntegrationEvent>(x =>
                x.FlightId == policy.DomainEvent.FlightId));
    }
}