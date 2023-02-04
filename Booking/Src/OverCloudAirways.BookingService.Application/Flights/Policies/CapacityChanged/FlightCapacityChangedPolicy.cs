using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.CapacityChanged;

public class FlightCapacityChangedPolicy : DomainEventPolicy<FlightCapacityChangedDomainEvent>
{
    public FlightCapacityChangedPolicy(FlightCapacityChangedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
