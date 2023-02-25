using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightSeatsReleasedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private int _seatsCount = 2;

    public FlightSeatsReleasedDomainEvent Build()
    {
        return new FlightSeatsReleasedDomainEvent(_flightId, _seatsCount);
    }

    public FlightSeatsReleasedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightSeatsReleasedDomainEventBuilder SetSeatsCount(int seatsCount)
    {
        _seatsCount = seatsCount;
        return this;
    }
}
