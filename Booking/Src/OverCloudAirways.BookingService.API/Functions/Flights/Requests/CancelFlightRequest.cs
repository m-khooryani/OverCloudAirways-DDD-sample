using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class CancelFlightRequest
{
    public FlightId FlightId { get; init; }
}
