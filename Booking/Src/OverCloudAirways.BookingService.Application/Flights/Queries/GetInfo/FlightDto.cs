namespace OverCloudAirways.BookingService.Application.Flights.Queries.GetInfo;

public record FlightDto(
    Guid Id,
    string Number,
    string DepartureAirport,
    string DestinationAirport,
    DateTimeOffset DepartureTime,
    DateTimeOffset ArrivalTime,
    string Route,
    int Distance,
    string AircraftModel,
    int AvailableSeats,
    int BookedSeats,
    double MaximumLuggageWeight,
    decimal EconomyPrice,
    decimal FirstClassPrice);
