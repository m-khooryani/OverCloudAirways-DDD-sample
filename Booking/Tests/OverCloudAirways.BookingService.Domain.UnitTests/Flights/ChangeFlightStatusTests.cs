using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ChangeFlightStatusTests : Test
{
    [Fact]
    public async Task Cancel_Given_Valid_Input_Should_Successfully_Cancel_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();

        // Act
        flight.ChangeStatus(FlightStatus.Departed);

        // Assert
        Assert.Equal(FlightStatus.Departed, flight.Status);
        AssertPublishedDomainEvent<FlightStatusChangedDomainEvent>(flight);
    }
}