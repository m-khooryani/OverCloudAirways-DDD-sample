using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class ChangeFlightStatusRequest
{
    public FlightId FlightId { get; init; }
    public FlightStatus Status { get; init; }
}
