using OverCloudAirways.BookingService.Application.Tickets.Queries.GetInfo;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BookingService.IntegrationTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BookingService.TestHelpers.Customers;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BookingService.TestHelpers.Tickets;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.BookingService.IntegrationTests.Tickets;

[Collection("Booking")]
public class TicketTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public TicketTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task IssueTicket_TicketShouldBeIssued_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var departureAirportId = AirportId.New();
        var destinationAirportId = AirportId.New();
        var departureAirportCode = "SRC";
        var destinationAirportCode = "DST";
        var aircraftId = AircraftId.New();
        var flightId = FlightId.New();
        var customerId = CustomerId.New();
        var ticketId = TicketId.New();
        var customerFirstName = "John";
        var customerLastName = "Doe";
        var departureTime = Clock.Now.AddDays(1);
        var arrivalTime = Clock.Now.AddDays(2);
        var flightNumber = "AA123";

        // Create Customer 
        var createCustomerCommand = new CreateCustomerCommandBuilder()
            .SetCustomerId(customerId)
            .Build();
        await _invoker.CommandAsync(createCustomerCommand);

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
            .SetNumber(flightNumber)
            .SetAircraftId(aircraftId)
            .SetArrivalTime(arrivalTime)
            .SetDepartureTime(departureTime)
            .SetDepartureAirportId(departureAirportId)
            .SetDestinationAirportId(destinationAirportId)
            .Build();
        await _invoker.CommandAsync(scheduleFlightCommand);

        // Issue Ticket
        var issueTicketCommand = new IssueTicketCommandBuilder()
            .SetTicketId(ticketId)
            .SetCustomerId(customerId)
            .SetFlightId(flightId)
            .Build();
        await _invoker.CommandAsync(issueTicketCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Ticket Query
        var query = new GetTicketInfoQuery(ticketId.Value);
        var ticket = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(ticket);
        Assert.Equal(ticketId.Value, ticket.Id);
        Assert.Equal(destinationAirportCode, ticket.DestinationAirportCode);
        Assert.Equal(departureAirportCode, ticket.DepartureAirportCode);
        Assert.Equal(customerFirstName, ticket.CustomerFirstName);
        Assert.Equal(customerLastName, ticket.CustomerLastName);
        Assert.Equal(arrivalTime, ticket.FlightArrivalTime);
        Assert.Equal(departureTime, ticket.FlightDepartureTime);
        Assert.Equal(flightNumber, ticket.FlightNumber);
    }
}
