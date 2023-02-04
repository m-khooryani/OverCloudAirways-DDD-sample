using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ReplacingFlightAircraftTests : FlightTests
{
    [Fact]
    public async Task ReplaceAircraft_Given_CanceledFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        await flight.CancelAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyScheduledFlightCanBeModifiedRule>(async () =>
        {
            await flight.ReplaceAircraftAsync(default, AircraftId.New());
        });
    }

    [Fact]
    public async Task ReplaceAircraft_Given_Valid_Input_Should_Successfully_ReplaceAircraft_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        var replacedAircraftId = AircraftId.New();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .Returns(new AircraftBuilder().Build());

        // Act
        await flight.ReplaceAircraftAsync(aggregateRepository, replacedAircraftId);

        // Assert
        Assert.Equal(replacedAircraftId, flight.AircraftId);
        AssertPublishedDomainEvent<FlightAircraftReplacedDomainEvent>(flight);
    }

    [Fact]
    public async Task ReplaceAircraft_Given_Non_Existin_AircraftId_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        var replacedAircraftId = AircraftId.New();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .ReturnsNull();

        // Act, Assert
        await AssertViolatedRuleAsync<FlightMustHaveExistingAircraftInTheSystemRule>(async () =>
        {
            await flight.ReplaceAircraftAsync(aggregateRepository, AircraftId.New());
        });
    }
}