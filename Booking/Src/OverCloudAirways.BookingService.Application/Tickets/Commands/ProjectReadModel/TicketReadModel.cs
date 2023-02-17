using OverCloudAirways.BuildingBlocks.Application.Models;

namespace OverCloudAirways.BookingService.Application.Tickets.Commands.ProjectReadModel;

internal record TicketReadModel(
    Guid Id,
    string CustomerFirstName,
    string CustomerLastName,
    string FlightNumber,
    DateTimeOffset FlightDepartureTime,
    DateTimeOffset FlightArrivalTime,
    string DepartureAirportCode,
    string DestinationAirportCode) : ReadModel(Id.ToString(), Id.ToString());