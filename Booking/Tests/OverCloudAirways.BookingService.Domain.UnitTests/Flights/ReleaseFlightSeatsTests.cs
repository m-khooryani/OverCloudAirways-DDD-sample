using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers._Shared;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ReleaseFlightSeatsTests : Test
{
    [Fact]
    public async Task ReleaseSeats_Given_CanceledFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        await flight.CancelAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyScheduledFlightCanBeModifiedRule>(async () =>
        {
            await flight.ReleaseSeatsAsync(1000);
        });
    }

    [Fact]
    public async Task ReleaseSeats_Given_Valid_Input_Should_Successfully_Release_Seats_And_Publish_Event()
    {
        // Arrange
        const int SeatsCount = 2;
        var passengers = new List<Passenger>()
        {
            new PassengerBuilder().Build(),
            new PassengerBuilder().Build()
        };
        var flight = await GetFlight();

        // Act
        await flight.ReserveSeatsAsync(CustomerId.New(), passengers);
        await flight.ReleaseSeatsAsync(SeatsCount);

        // Assert
        Assert.Equal(300, flight.AvailableSeats);
        Assert.Equal(0, flight.ReservedSeats);
        AssertPublishedDomainEvent<FlightSeatsReleasedDomainEvent>(flight);
    }
}