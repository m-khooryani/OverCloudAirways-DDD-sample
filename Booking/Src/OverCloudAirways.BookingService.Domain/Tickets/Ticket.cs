using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BookingService.Domain.Tickets.Rules;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.BookingService.Domain.Tickets;

public class Ticket : AggregateRoot<TicketId>
{
    public FlightId FlightId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public string SeatNumber { get; private set; }

    private Ticket()
    {
    }

    public static async Task<Ticket> IssueAsync(
        IAggregateRepository aggregateRepository,
        ITicketSeatNumberGeneratorService seatNumberGenerator,
        TicketId ticketId,
        FlightId flightId,
        CustomerId customerId)
    {
        await CheckRuleAsync(new TicketMustBeIssuedOnlyForFlightsInTheSystemRule(flightId, aggregateRepository));
        await CheckRuleAsync(new TicketMustBeIssuedOnlyForCustomersInTheSystemRule(customerId, aggregateRepository));

        var seatNumber = await seatNumberGenerator.GenerateAsync();
        var @event = new TicketIssuedDomainEvent(ticketId, flightId, customerId, seatNumber);

        var ticket = new Ticket();
        ticket.Apply(@event);

        return ticket;
    }

    protected void When(TicketIssuedDomainEvent @event)
    {
        Id = @event.TicketId;
        FlightId = @event.FlightId;
        CustomerId = @event.CustomerId;
        SeatNumber = @event.SeatNumber;
    }
}
