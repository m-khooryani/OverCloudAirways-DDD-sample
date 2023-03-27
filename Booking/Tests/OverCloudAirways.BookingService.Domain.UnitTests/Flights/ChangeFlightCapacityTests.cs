using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers._Shared;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ChangeFlightCapacityTests : Test
{
    [Fact]
    public async Task ChangeCapacity_Given_Valid_Input_Should_Successfully_ChangesCapacity_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();

        // Act
        await flight.ChangeCapacityAsync(50);

        // Assert
        Assert.Equal(50, flight.AvailableSeats);
        AssertPublishedDomainEvent<FlightCapacityChangedDomainEvent>(flight);
    }

    [Fact]
    public async Task ChangeCapacity_Given_Input_Lower_Than_Already_BookedSeats_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        var passengers = new List<Passenger>()
        {
            new PassengerBuilder().Build(),
            new PassengerBuilder().Build()
        };
        var seatsLowerThanBooked = passengers.Count - 1;
        await flight.ReserveSeatsAsync(CustomerId.New(), passengers);

        // Act, Assert
        await AssertViolatedRuleAsync<FlightCapacityMustBeGreaterEqualThanTotalBookedReservedSeatsRule>(async () =>
        {
            await flight.ChangeCapacityAsync(seatsLowerThanBooked);
        });
    }
}