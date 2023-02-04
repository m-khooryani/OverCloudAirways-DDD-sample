using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightCanceledDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();

    public FlightCanceledDomainEvent Build()
    {
        return new FlightCanceledDomainEvent(_flightId);
    }

    public FlightCanceledDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }
}