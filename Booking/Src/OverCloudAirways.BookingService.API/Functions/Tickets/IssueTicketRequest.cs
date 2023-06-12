using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Tickets;

public class IssueTicketRequest
{
    public FlightId FlightId { get; init; }
    public CustomerId CustomerId { get; init; }
}

