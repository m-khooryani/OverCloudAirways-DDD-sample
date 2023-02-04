using OverCloudAirways.BookingService.Application.Flights.Policies.AircraftReplaced;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightAircraftReplacedPolicyBuilder
{
    private FlightAircraftReplacedDomainEvent _domainEvent = new FlightAircraftReplacedDomainEventBuilder().Build();

    public FlightAircraftReplacedPolicy Build()
    {
        return new FlightAircraftReplacedPolicy(_domainEvent);
    }

    public FlightAircraftReplacedPolicyBuilder SetDomainEvent(FlightAircraftReplacedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}