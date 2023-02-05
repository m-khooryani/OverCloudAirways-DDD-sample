using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightStatusChangedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private FlightStatus _status = FlightStatus.Departed;

    public FlightStatusChangedDomainEvent Build()
    {
        return new FlightStatusChangedDomainEvent(_flightId, _status);
    }

    public FlightStatusChangedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightStatusChangedDomainEventBuilder SetStatus(FlightStatus flightStatus)
    {
        _status = flightStatus;
        return this;
    }
}
