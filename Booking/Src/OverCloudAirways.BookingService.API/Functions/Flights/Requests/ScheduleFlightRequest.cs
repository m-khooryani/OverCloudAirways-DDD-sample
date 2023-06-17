using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Airports;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class ScheduleFlightRequest
{
    public string Number { get; init; }
    public AirportId DepartureAirportId { get; init; }
    public AirportId DestinationAirportId { get; init; }
    public DateTimeOffset DepartureTime { get; init; }
    public DateTimeOffset ArrivalTime { get; init; }
    public string Route { get; init; }
    public int Distance { get; init; }
    public AircraftId AircraftId { get; init; }
    public int AvailableSeats { get; init; }
    public double MaximumLuggageWeight { get; init; }
}
