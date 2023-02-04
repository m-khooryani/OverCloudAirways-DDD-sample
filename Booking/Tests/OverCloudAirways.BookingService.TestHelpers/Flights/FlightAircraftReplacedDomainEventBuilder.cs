using OverCloudAirways.BookingService.Domain.Aircrafts;
using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightAircraftReplacedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private AircraftId _aircraftId = AircraftId.New();

    public FlightAircraftReplacedDomainEvent Build()
    {
        return new FlightAircraftReplacedDomainEvent(_flightId, _aircraftId);
    }

    public FlightAircraftReplacedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightAircraftReplacedDomainEventBuilder SetAircraftId(AircraftId aircraftId)
    {
        _aircraftId = aircraftId;
        return this;
    }
}
