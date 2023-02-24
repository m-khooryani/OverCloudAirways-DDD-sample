using NSubstitute;
using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.Domain.FlightBookings.Rules;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.FlightBookings;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.FlightBookings;

public class CancelFlightBookingTests : Test
{
    [Fact]
    public async Task Cancel_Given_DepartedFlight_Input_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        var flightBooking = await new FlightBookingBuilder()
            .SetFlight(flight)
            .BuildAsync();
        flight.ChangeStatus(FlightStatus.Arrived);
        var repository = Substitute.For<IAggregateRepository>();
        repository
            .LoadAsync<Flight, FlightId>(flight.Id)
            .Returns(flight);

        // Act, Assert
        await AssertViolatedRuleAsync<FlightBookingCanOnlyBeCancelledForFlightsHasNotYetDepartedRule>(async () =>
        {
            await flightBooking.CancelAsync(repository);
        });
    }

    [Fact]
    public async Task Cancel_Given_Valid_Input_Should_Successfully_Cancel_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();
        var flightBooking = await new FlightBookingBuilder()
            .SetFlight(flight)
            .BuildAsync();
        var repository = Substitute.For<IAggregateRepository>();
        repository
            .LoadAsync<Flight, FlightId>(flight.Id)
            .Returns(flight);

        // Act
        await flightBooking.CancelAsync(repository);

        // Assert
        Assert.Equal(FlightBookingStatus.Cancelled, flightBooking.Status);
        AssertPublishedDomainEvent<FlightBookingCancelledDomainEvent>(flightBooking);
    }
}
