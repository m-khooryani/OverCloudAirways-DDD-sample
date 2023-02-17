using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BookingService.Application.Tickets.Commands.Issue;

namespace OverCloudAirways.BookingService.TestHelpers.Tickets;

public class IssueTicketCommandBuilder
{
    private TicketId _ticketId = TicketId.New();
    private FlightId _flightId = FlightId.New();
    private CustomerId _customerId = CustomerId.New();

    public IssueTicketCommand Build()
    {
        return new IssueTicketCommand(_ticketId, _flightId, _customerId);
    }

    public IssueTicketCommandBuilder SetTicketId(TicketId ticketId)
    {
        _ticketId = ticketId;
        return this;
    }

    public IssueTicketCommandBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public IssueTicketCommandBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }
}
