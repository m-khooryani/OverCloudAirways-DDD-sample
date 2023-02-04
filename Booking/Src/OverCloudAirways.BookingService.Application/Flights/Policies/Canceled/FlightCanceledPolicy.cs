using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.Canceled;

public class FlightCanceledPolicy : DomainEventPolicy<FlightCanceledDomainEvent>
{
    public FlightCanceledPolicy(FlightCanceledDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
