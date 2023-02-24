using NSubstitute;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BookingService.Domain.Flights.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class ChargeFlightPriceTests : Test
{
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
}