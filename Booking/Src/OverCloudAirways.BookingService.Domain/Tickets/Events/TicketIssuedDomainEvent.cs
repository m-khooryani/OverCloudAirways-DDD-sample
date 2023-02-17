using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.BookingService.Domain.Tickets.Events;

public record TicketIssuedDomainEvent(
    TicketId TicketId,
    FlightId FlightId,
    CustomerId CustomerId,
    string SeatNumber) : DomainEvent(TicketId);
