using NSubstitute;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class FlightTests : Test
{
    [Fact]
    public async Task ScheduleFlight_Given_DepartureTime_In_The_Past_Should_Throw_Business_Error()
    {
        // Arrange
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        var departureTime = Clock.Now.AddDays(-1);
        var builder = new FlightBuilder()
            .SetAggregateRepository(aggregateRepository)
            .SetDepartureTime(departureTime);

        // Act, Assert
        await AssertViolatedRuleAsync<FlightCanOnlyBeScheduledInTheFutureRule>(async () =>
        {
            _ = await builder.BuildAsync();
        });
    }

    [Fact]
    public async Task ScheduleFlight_Given_Aircraft_Range_Less_Than_Distance_Should_Throw_Business_Error()
    {
        // Arrange
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .Returns(new AircraftBuilder().Build());
        var builder = new FlightBuilder()
            .SetAggregateRepository(aggregateRepository)
            .SetDistance(99999);

        // Act, Assert
        await AssertViolatedRuleAsync<TheAircraftsRangeMustBeGreaterThanTheFlightDistanceRule>(async () =>
        {
            _ = await builder.BuildAsync();
        });
    }

    [Fact]
    public async Task ScheduleFlight_Given_Invalid_Airport_Ids_Should_Throw_Business_Error()
    {
        // Arrange
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .Returns(new AircraftBuilder().Build());
        var builder = new FlightBuilder()
            .SetAggregateRepository(aggregateRepository);

        // Act, Assert
        await AssertViolatedRuleAsync<FlightMustBeScheduledBetweenTwoExistingAirportsInTheSystemRule>(async () =>
        {
            _ = await builder.BuildAsync();
        });
    }
}
