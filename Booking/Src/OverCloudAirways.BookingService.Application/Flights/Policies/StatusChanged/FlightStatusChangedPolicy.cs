using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.StatusChanged;

public class FlightStatusChangedPolicy : DomainEventPolicy<FlightStatusChangedDomainEvent>
{
    public FlightStatusChangedPolicy(FlightStatusChangedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
