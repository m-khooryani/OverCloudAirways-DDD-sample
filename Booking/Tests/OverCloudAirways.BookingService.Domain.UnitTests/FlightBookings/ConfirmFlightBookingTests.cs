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

public class ConfirmFlightBookingTests : Test
{
    [Fact]
    public async Task Confirm_Given_DepartedFlight_Input_Should_Throw_Business_Error()
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
        await AssertViolatedRuleAsync<FlightBookingCanOnlyBeConfirmedForFlightsHasNotYetDepartedRule>(async () =>
        {
            await flightBooking.ConfirmAsync(repository);
        });
    }

    [Fact]
    public async Task Confirm_Given_Valid_Input_Should_Successfully_Confirm_And_Publish_Event()
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
        await flightBooking.ConfirmAsync(repository);

        // Assert
        Assert.Equal(FlightBookingStatus.Confirmed, flightBooking.Status);
        AssertPublishedDomainEvent<FlightBookingConfirmedDomainEvent>(flightBooking);
    }

    [Fact]
    public async Task Confirm_Given_AlreadyConfirmed_Input_Should_Throw_Business_Error()
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

        await flightBooking.ConfirmAsync(repository);

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyReservedFlightBookingsCanBeConfirmedRule>(async () =>
        {
            await flightBooking.ConfirmAsync(repository);
        });
    }
}
