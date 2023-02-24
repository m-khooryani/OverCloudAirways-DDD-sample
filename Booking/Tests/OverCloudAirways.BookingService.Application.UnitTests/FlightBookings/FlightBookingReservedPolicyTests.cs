using NSubstitute;
using OverCloudAirways.BookingService.Application.FlightBookings.Policies.Reserved;
using OverCloudAirways.BookingService.IntegrationEvents.FlightBookings;
using OverCloudAirways.BookingService.TestHelpers.FlightBookings;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.FlightBookings;

public class FlightBookingReservedPolicyTests
{
    [Fact]
    public async Task PublishEventFlightBookingReservedPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new PublishEventFlightBookingReservedPolicyHandler(commandsScheduler);
        var policy = new FlightBookingReservedPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueuePublishingEventAsync(Arg.Is<FlightBookingReservedIntegrationEvent>(x =>
                x.FlightId == policy.DomainEvent.FlightId &&
                x.CustomerId == policy.DomainEvent.CustomerId &&
                x.Passengers == policy.DomainEvent.Passengers &&
                x.FlightBookingId == policy.DomainEvent.FlightBookingId));
    }
}
