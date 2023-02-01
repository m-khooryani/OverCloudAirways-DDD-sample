using OverCloudAirways.BookingService.Application.Flights.Commands.ReserveSeats;
using OverCloudAirways.BookingService.Application.Flights.Queries.GetInfo;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.IntegrationTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.BookingService.IntegrationTests.Flights;

[Collection("Booking")]
public class FlightTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public FlightTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task ScheduleFlight_FlightShouldBeScheduled_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var departureAirportId = AirportId.New();
        var destinationAirportId = AirportId.New();
        var departureAirportCode = "SRC";
        var destinationAirportCode = "DST";
        var aircraftId = AircraftId.New();
        var flightId = FlightId.New();

        // Create Departure Airport 
        var createDepartureAirportCommand = new CreateAirportCommandBuilder()
            .SetId(departureAirportId)
            .SetCode(departureAirportCode)
            .Build();
        await _invoker.CommandAsync(createDepartureAirportCommand);

        // Create Destination Airport 
        var createDestinationAirportCommand = new CreateAirportCommandBuilder()
            .SetId(destinationAirportId)
            .SetCode(destinationAirportCode)
            .Build();
        await _invoker.CommandAsync(createDestinationAirportCommand);

        // Create Aircraft 
        var createAircraftCommand = new CreateAircraftCommandBuilder()
            .SetAircraftId(aircraftId)
            .Build();
        await _invoker.CommandAsync(createAircraftCommand);

        // Schedule Flight
        var scheduleFlightCommand = new ScheduleFlightCommandBuilder()
            .SetFlightId(flightId)
            .SetAircraftId(aircraftId)
            .SetDepartureAirportId(departureAirportId)
            .SetDestinationAirportId(destinationAirportId)
            .Build();
        await _invoker.CommandAsync(scheduleFlightCommand);

        // Flight Scheduled Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Charge Flight Price Command
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Flight Price Charged Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Project Flight Read Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Flight Query
        var query = new GetFlightInfoQuery(flightId.Value);
        var flight = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(flight);
        Assert.Equal(scheduleFlightCommand.FlightId.Value, flight.Id);
        Assert.Equal(createAircraftCommand.Model, flight.AircraftModel);
        Assert.Equal(scheduleFlightCommand.ArrivalTime, flight.ArrivalTime);
        Assert.Equal(scheduleFlightCommand.AvailableSeats, flight.AvailableSeats);
        Assert.Equal(createDepartureAirportCommand.Code, flight.DepartureAirport);
        Assert.Equal(scheduleFlightCommand.DepartureTime, flight.DepartureTime);
        Assert.Equal(createDestinationAirportCommand.Code, flight.DestinationAirport);
        Assert.Equal(scheduleFlightCommand.Distance, flight.Distance);
        Assert.True(flight.EconomyPrice > 0M);
        Assert.True(flight.FirstClassPrice > 0M);

        // Reserve Flight
        var reserveFlightSeatsCommand = new ReserveFlightSeatsCommand(flightId, 2);
        await _invoker.CommandAsync(reserveFlightSeatsCommand);
        // Flight Scheduled Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Publish Integration Event
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Charge Flight Price Command
        await _testFixture.ProcessLastOutboxMessageAsync();

        query = new GetFlightInfoQuery(flightId.Value);
        flight = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(flight);
        Assert.Equal(scheduleFlightCommand.FlightId.Value, flight.Id);
        Assert.Equal(createAircraftCommand.Model, flight.AircraftModel);
        Assert.Equal(scheduleFlightCommand.ArrivalTime, flight.ArrivalTime);
        Assert.Equal(scheduleFlightCommand.AvailableSeats - 2, flight.AvailableSeats);
        Assert.Equal(createDepartureAirportCommand.Code, flight.DepartureAirport);
        Assert.Equal(scheduleFlightCommand.DepartureTime, flight.DepartureTime);
        Assert.Equal(createDestinationAirportCommand.Code, flight.DestinationAirport);
        Assert.Equal(scheduleFlightCommand.Distance, flight.Distance);
        Assert.True(flight.EconomyPrice > 0M);
        Assert.True(flight.FirstClassPrice > 0M);
    }
}
