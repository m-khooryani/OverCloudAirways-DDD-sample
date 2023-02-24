using OverCloudAirways.BookingService.Domain.FlightBookings;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BookingService.Domain.FlightBookings.Rules;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.FlightBookings;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.FlightBookings;

public class ReserveFlightBookingTests : Test
{
    [Fact]
    public async Task Book_Given_Valid_Input_Should_Successfully_Book_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();

        // Act
        var flightBooking = await new FlightBookingBuilder()
            .SetFlight(flight)
            .BuildAsync();

        // Assert
        Assert.Equal(FlightBookingStatus.Reserved, flightBooking.Status);
        AssertPublishedDomainEvent<FlightBookingReservedDomainEvent>(flightBooking);
    }

    [Fact]
    public async Task Book_Given_DepartedFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        flight.ChangeStatus(FlightStatus.Arrived);

        var flightBookingBuilder = new FlightBookingBuilder()
            .SetFlight(flight);

        // Act, Assert
        await AssertViolatedRuleAsync<FlightBookingCanOnlyBeReservedForFlightsHasNotYetDepartedRule>(async () =>
        {
            await flightBookingBuilder.BuildAsync();
        });
    }
}
