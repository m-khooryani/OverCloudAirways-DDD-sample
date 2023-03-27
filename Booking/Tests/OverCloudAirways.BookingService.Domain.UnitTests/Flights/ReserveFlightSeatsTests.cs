using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers._Shared;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ReserveFlightSeatsTests : Test
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
            await flight.ReserveSeatsAsync(CustomerId.New(), new List<Passenger>());
        });
    }

    [Fact]
    public async Task ReserveSeats_Given_More_Than_Available_Seats_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight(1);
        var passengers = new List<Passenger>()
        {
            new PassengerBuilder().Build(),
            new PassengerBuilder().Build()
        };

        // Act, Assert
        await AssertViolatedRuleAsync<FlightMustHaveEnoughAvailableSeatsToReserveRule>(async () =>
        {
            await flight.ReserveSeatsAsync(CustomerId.New(), passengers);
        });
    }

    [Fact]
    public async Task ReserveSeats_Given_Valid_Input_Should_Successfully_Reserve_Seats_And_Publish_Event()
    {
        // Arrange
        var passengers = new List<Passenger>()
        {
            new PassengerBuilder().Build(),
            new PassengerBuilder().Build()
        };
        var flight = await GetFlight();

        // Act
        await flight.ReserveSeatsAsync(CustomerId.New(), passengers);

        // Assert
        Assert.Equal(300 - passengers.Count, flight.AvailableSeats);
        Assert.Equal(2, flight.ReservedSeats);
        AssertPublishedDomainEvent<FlightSeatsReservedDomainEvent>(flight);
    }
}
