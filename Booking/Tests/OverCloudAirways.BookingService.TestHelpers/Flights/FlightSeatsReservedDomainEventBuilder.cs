using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightSeatsReservedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private int _seatsCount = 2;

    public FlightSeatsReservedDomainEvent Build()
    {
        return new FlightSeatsReservedDomainEvent(_flightId, _seatsCount);
    }

    public FlightSeatsReservedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightSeatsReservedDomainEventBuilder SetSeatsCount(int seatsCount)
    {
        _seatsCount = seatsCount;
        return this;
    }
}
