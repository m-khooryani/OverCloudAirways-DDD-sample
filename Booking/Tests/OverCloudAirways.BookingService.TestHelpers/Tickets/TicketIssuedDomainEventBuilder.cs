using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BookingService.Domain.Tickets;

namespace OverCloudAirways.BookingService.TestHelpers.Tickets;

public class TicketIssuedDomainEventBuilder
{
    private TicketId _ticketId = TicketId.New();
    private FlightId _flightId = FlightId.New();
    private CustomerId _customerId = CustomerId.New();
    private string _seatNumber = "A14";

    public TicketIssuedDomainEvent Build()
    {
        return new TicketIssuedDomainEvent(_ticketId, _flightId, _customerId, _seatNumber);
    }

    public TicketIssuedDomainEventBuilder SetTicketId(TicketId ticketId)
    {
        _ticketId = ticketId;
        return this;
    }

    public TicketIssuedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public TicketIssuedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public TicketIssuedDomainEventBuilder SetSeatNumber(string seatNumber)
    {
        _seatNumber = seatNumber;
        return this;
    }
}
