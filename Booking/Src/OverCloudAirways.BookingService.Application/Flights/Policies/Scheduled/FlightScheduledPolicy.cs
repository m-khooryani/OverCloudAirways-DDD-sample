using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.Scheduled;

public class FlightScheduledPolicy : DomainEventPolicy<FlightScheduledDomainEvent>
{
    public FlightScheduledPolicy(FlightScheduledDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
