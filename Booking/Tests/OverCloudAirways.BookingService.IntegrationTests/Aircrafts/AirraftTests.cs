using OverCloudAirways.BookingService.Application.Aircrafts.Queries.GetInfo;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.IntegrationTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.BookingService.IntegrationTests.Aircrafts;

[Collection("Booking")]
public class AirraftTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public AirraftTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task CreateAircraft_AircraftShouldBeCreated_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var aircraftId = AircraftId.New();

        // Create Aircraft 
        var createAircraftCommand = new CreateAircraftCommandBuilder()
            .SetAircraftId(aircraftId)
            .Build();
        await _invoker.CommandAsync(createAircraftCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // ReadUser Query
        var query = new GetAircraftInfoQuery(aircraftId.Value);
        var aircraft = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(aircraft);
        Assert.Equal(aircraftId.Value, aircraft.AircraftId); 
        Assert.Equal(createAircraftCommand.Type, aircraft.Type);
        Assert.Equal(createAircraftCommand.Manufacturer, aircraft.Manufacturer);
        Assert.Equal(createAircraftCommand.Model, aircraft.Model);
        Assert.Equal(createAircraftCommand.SeatingCapacity, aircraft.SeatingCapacity);
        Assert.Equal(createAircraftCommand.EconomyCostPerKM, aircraft.EconomyCostPerKM);
        Assert.Equal(createAircraftCommand.FirstClassCostPerKM, aircraft.FirstClassCostPerKM);
        Assert.Equal(createAircraftCommand.Range, aircraft.Range);
        Assert.Equal(createAircraftCommand.CruisingAltitude, aircraft.CruisingAltitude);
        Assert.Equal(createAircraftCommand.MaxTakeoffWeight, aircraft.MaxTakeoffWeight);
        Assert.Equal(createAircraftCommand.Length, aircraft.Length);
        Assert.Equal(createAircraftCommand.Wingspan, aircraft.Wingspan);
        Assert.Equal(createAircraftCommand.Height, aircraft.Height);
        Assert.Equal(2, aircraft.Engines.Count); 
        Assert.True(createAircraftCommand.Engines.SequenceEqual(aircraft.Engines));
    }
}

