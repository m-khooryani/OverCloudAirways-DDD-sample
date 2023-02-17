namespace OverCloudAirways.BookingService.Application.Tickets.Queries.GetInfo;

public record TicketDto(
    Guid Id,
    string CustomerFirstName,
    string CustomerLastName,
    string FlightNumber,
    DateTimeOffset FlightDepartureTime,
    DateTimeOffset FlightArrivalTime,
    string DepartureAirportCode,
    string DestinationAirportCode);
