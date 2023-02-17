using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BookingService.Domain.Tickets.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Aircrafts;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BookingService.TestHelpers.Customers;
using OverCloudAirways.BookingService.TestHelpers.Flights;
using OverCloudAirways.BookingService.TestHelpers.Tickets;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Tickets;

public class TicketTests : Test
{
    [Fact]
    public async Task Issue_Given_Invalid_FlightId_Should_Throw_Business_Error()
    {
        // Arrange
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Flight, FlightId>(Arg.Any<FlightId>())
            .ReturnsNull();
        var ticketBuilder = new TicketBuilder()
            .SetAggregateRepository(aggregateRepository);

        // Act, Assert
        await AssertViolatedRuleAsync<TicketMustBeIssuedOnlyForFlightsInTheSystemRule>(async () =>
        {
            await ticketBuilder.BuildAsync();
        });
    }

    [Fact]
    public async Task Issue_Given_Invalid_CustomerId_Should_Throw_Business_Error()
    {
        // Arrange
        var flight = await GetFlight();
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Flight, FlightId>(Arg.Any<FlightId>())
            .Returns(flight);
        var ticketBuilder = new TicketBuilder()
            .SetAggregateRepository(aggregateRepository);

        // Act, Assert
        await AssertViolatedRuleAsync<TicketMustBeIssuedOnlyForCustomersInTheSystemRule>(async () =>
        {
            await ticketBuilder.BuildAsync();
        });
    }

    [Fact]
    public async Task Issue_Given_Valid_Input_Should_Successfully_Issue_And_Publish_Event()
    {
        // Arrange
        var flightId = FlightId.New();
        var flight = await GetFlight(flightId);
        var aggregateRepository = Substitute.For<IAggregateRepository>();
        aggregateRepository
            .LoadAsync<Flight, FlightId>(Arg.Any<FlightId>())
            .Returns(flight);
        var customerId = CustomerId.New();
        var customer = new CustomerBuilder().SetCustomerId(customerId).Build();
        aggregateRepository
            .LoadAsync<Customer, CustomerId>(Arg.Any<CustomerId>())
            .Returns(customer);
        var ticketBuilder = new TicketBuilder()
            .SetAggregateRepository(aggregateRepository);

        var seatNumberGenerator = Substitute.For<ITicketSeatNumberGeneratorService>();
        seatNumberGenerator.GenerateAsync().Returns("1A");

        // Act
        var ticket = await ticketBuilder
            .SetAggregateRepository(aggregateRepository)
            .SetTicketSeatNumberGeneratorService(seatNumberGenerator)
            .SetCustomerId(customerId)
            .SetFlightId(flightId)
            .BuildAsync();

        // Assert
        Assert.Equal(customerId, ticket.CustomerId);
        Assert.Equal(flightId, ticket.FlightId);
        AssertPublishedDomainEvent<TicketIssuedDomainEvent>(ticket);
    }

    private static async Task<Flight> GetFlight(FlightId? flightId = null)
    {
        flightId ??= FlightId.New();
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

        var flight = await builder
            .SetFlightId(flightId)
            .BuildAsync();
        return flight;
    }
}
