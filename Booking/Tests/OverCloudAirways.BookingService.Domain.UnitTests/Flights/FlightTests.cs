using NSubstitute;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Flights;

public class FlightTests : Test
{
    // helper
    protected static async Task<Flight> GetFlight()
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
