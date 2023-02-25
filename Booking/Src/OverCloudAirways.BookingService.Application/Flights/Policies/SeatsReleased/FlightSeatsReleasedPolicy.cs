using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReleased;

public class FlightSeatsReleasedPolicy : DomainEventPolicy<FlightSeatsReleasedDomainEvent>
{
    public FlightSeatsReleasedPolicy(FlightSeatsReleasedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
