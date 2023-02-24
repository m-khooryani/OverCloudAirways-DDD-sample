using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class CancelFlightTests : Test
{
    [Fact]
    public async Task Cancel_Given_Valid_Input_Should_Successfully_Cancel_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();

        // Act
        await flight.CancelAsync();

        // Assert
        Assert.Equal(FlightStatus.Cancelled, flight.Status);
        AssertPublishedDomainEvent<FlightCanceledDomainEvent>(flight);
    }

    [Fact]
    public async Task Cancel_Given_Given_CanceledFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        await flight.CancelAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyScheduledFlightCanBeModifiedRule>(async () =>
        {
            await flight.CancelAsync();
        });
    }
}
