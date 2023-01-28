using OverCloudAirways.BookingService.Application.Airports.Queries.GetInfo;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.IntegrationTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.BookingService.IntegrationTests.Airports;

[Collection("Booking")]
public class AirportTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public AirportTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task CreateAirport_AirportShouldBeCreated_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var airportId = AirportId.New();
        var airportCode = "JFK";

        // Create Airport 
        var createAirportCommand = new CreateAirportCommandBuilder()
            .SetId(airportId)
            .SetCode(airportCode)
            .Build();
        await _invoker.CommandAsync(createAirportCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Airport Query
        var query = new GetAirportInfoQuery(airportCode);
        var airport = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(airport);
        Assert.Equal(airportId.Value, airport.Id);
        Assert.Equal(createAirportCommand.Code, airport.Code);
        Assert.Equal(createAirportCommand.Name, airport.Name);
        Assert.Equal(createAirportCommand.Location, airport.Location);
        Assert.True(createAirportCommand.Terminals.SequenceEqual(airport.Terminals));
    }
}
