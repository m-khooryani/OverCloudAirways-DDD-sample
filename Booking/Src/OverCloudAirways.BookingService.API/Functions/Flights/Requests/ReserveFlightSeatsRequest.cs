using OverCloudAirways.BookingService.Domain._Shared;
using OverCloudAirways.BookingService.Domain.Flights;

namespace OverCloudAirways.BookingService.API.Functions.Flights.Requests;

public class ReserveFlightSeatsRequest
{
    public FlightId FlightId { get; init; }
    public IReadOnlyList<Passenger> Passengers { get; init; }
}
