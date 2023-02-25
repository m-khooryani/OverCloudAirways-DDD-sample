using NSubstitute;
using OverCloudAirways.BookingService.Application.FlightBookings.Policies.Cancelled;
using OverCloudAirways.BookingService.Application.Flights.Commands.ReleaseSeats;
using OverCloudAirways.BookingService.TestHelpers.FlightBookings;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Application.UnitTests.FlightBookings;

public class FlightBookingCancelledPolicyTests
{
    [Fact]
    public async Task EnqueueReleaseFlightSeatsFlightBookingCancelledPolicyHandler_Should_Publish_Integration_Event()
    {
        // Arrange
        var commandsScheduler = Substitute.For<ICommandsScheduler>();
        var handler = new EnqueueReleaseFlightSeatsFlightBookingCancelledPolicyHandler(commandsScheduler);
        var policy = new FlightBookingCancelledPolicyBuilder().Build();

        // Act
        await handler.Handle(policy, CancellationToken.None);

        // Assert
        await commandsScheduler
            .Received(1)
            .EnqueueAsync(Arg.Is<ReleaseFlightSeatsCommand>(c => 
                c.FlightId == policy.DomainEvent.FlightId &&
                c.SeatsCount == policy.DomainEvent.SeatsCount));
    }
}
