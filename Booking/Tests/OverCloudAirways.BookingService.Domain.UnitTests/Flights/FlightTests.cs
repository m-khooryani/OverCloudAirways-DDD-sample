using NSubstitute;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
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

    [Fact]
    public async Task ChargePrice_Given_CanceledFlight_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        await flight.CancelAsync();

        // Act, Assert
        await AssertViolatedRuleAsync<OnlyScheduledFlightCanBeModifiedRule>(async () =>
        {
            await flight.ChargePriceAsync(Substitute.For<IFlightPriceCalculatorService>());
        });
    }

    [Fact]
    public async Task ChargePrice_Given_Valid_Input_Should_Successfully_Charge_Prices_And_Publish_Event()
    {
        // Arrange
        var flight = await GetFlight();
        var priceCalculator = Substitute.For<IFlightPriceCalculatorService>();
        const int EconomyPrice = 100;
        const int FirstClassPrice = 200;
        priceCalculator.CalculateAsync(flight).Returns((EconomyPrice, FirstClassPrice));

        // Act
        await flight.ChargePriceAsync(priceCalculator);

        // Assert
        Assert.Equal(EconomyPrice, flight.EconomyPrice);
        Assert.Equal(FirstClassPrice, flight.FirstClassPrice);
        AssertPublishedDomainEvent<FlightPriceChargedDomainEvent>(flight);
    }

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
        AssertPublishedDomainEvent<FlightSeatsReservedDomainEvent>(flight);
    }

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

    // helper
    private static async Task<Flight> GetFlight()
    {
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Aircraft, AircraftId>(Arg.Any<AircraftId>())
            .Returns(new AircraftBuilder().Build());
        var codeChecker = Substitute.For<IAirportCodeUniqueChecker>();
        codeChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);
        var airport = await new AirportBuilder()
            .SetAirportCodeUniqueChecker(codeChecker)
            .BuildAsync();
        aggregateRepository
            .LoadAsync<Airport, AirportId>(Arg.Any<AirportId>())
            .Returns(airport);
        var builder = new FlightBuilder()
            .SetAggregateRepository(aggregateRepository);
        var flight = await builder.BuildAsync();
        return flight;
    }
}
