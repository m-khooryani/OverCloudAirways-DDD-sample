using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class ReplaceFlightAircraftRequest
{
    public FlightId FlightId { get; init; }
    public AircraftId AircraftId { get; init; }
}
