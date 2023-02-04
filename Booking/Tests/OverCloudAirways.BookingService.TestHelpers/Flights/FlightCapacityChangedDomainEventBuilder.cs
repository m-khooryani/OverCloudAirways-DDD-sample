using OverCloudAirways.BookingService.Domain.Flights;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightCapacityChangedDomainEventBuilder
{
    private FlightId _flightId = FlightId.New();
    private int _capacity = 15;

    public FlightCapacityChangedDomainEvent Build()
    {
        return new FlightCapacityChangedDomainEvent(_flightId, _capacity);
    }

    public FlightCapacityChangedDomainEventBuilder SetFlightId(FlightId flightId)
    {
        _flightId = flightId;
        return this;
    }

    public FlightCapacityChangedDomainEventBuilder SetCapacity(int capacity)
    {
        _capacity = capacity;
        return this;
    }
}
