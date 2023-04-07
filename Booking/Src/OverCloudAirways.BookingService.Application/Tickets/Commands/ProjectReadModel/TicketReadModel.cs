using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;

internal record TicketReadModel(
    Guid TicketId,
    string CustomerFirstName,
    string CustomerLastName,
    string FlightNumber,
    DateTimeOffset FlightDepartureTime,
    DateTimeOffset FlightArrivalTime,
    string DepartureAirportCode,
    string DestinationAirportCode) : ReadModel(TicketId.ToString(), TicketId.ToString());