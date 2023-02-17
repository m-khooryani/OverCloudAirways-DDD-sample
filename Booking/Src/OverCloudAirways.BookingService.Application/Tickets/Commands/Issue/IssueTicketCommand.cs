using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Tickets;
using OverCloudAirways.BuildingBlocks.Application.Commands;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.Issue;

public record IssueTicketCommand(
    TicketId TicketId,
    FlightId FlightId,
    CustomerId CustomerId) : Command;
