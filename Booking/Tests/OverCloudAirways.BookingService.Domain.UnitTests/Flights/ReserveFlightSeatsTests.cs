using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ReserveFlightSeatsTests : FlightTests
{
    [Fact]
    public async Task ReserveSeats_Given_CanceledFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        await flight.CancelAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyScheduledFlightCanBeModifiedRule>(async () =>
        {
            await flight.ReserveSeatsAsync(1000);
        });
    }

    [Fact]
    public async Task ReserveSeats_Given_More_Than_Available_Seats_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();

        // Act, Assert
        await AssertViolatedRuleAsync<FlightMustHaveEnoughAvailableSeatsToReserveRule>(async () =>
        {
            await flight.ReserveSeatsAsync(1000);
        });
    }

    [Fact]
    public async Task ReserveSeats_Given_Valid_Input_Should_Successfully_Reserve_Seats_And_Publish_Event()
    {
        // Arrange
        const int SeatsToReserve = 2;
        var flight = await GetFlight();

        // Act
        await flight.ReserveSeatsAsync(SeatsToReserve);

        // Assert
        Assert.Equal(300 - SeatsToReserve, flight.AvailableSeats);
        Assert.Equal(2, flight.ReservedSeats);
        AssertPublishedDomainEvent<FlightSeatsReservedDomainEvent>(flight);
    }
}