using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.AircraftReplaced;

public class FlightAircraftReplacedPolicy : DomainEventPolicy<FlightAircraftReplacedDomainEvent>
{
    public FlightAircraftReplacedPolicy(FlightAircraftReplacedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
