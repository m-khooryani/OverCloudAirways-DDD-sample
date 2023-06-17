using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class ChangeFlightCapacityRequest
{
    public FlightId FlightId { get; init; }
    public int Capacity { get; init; }
}
