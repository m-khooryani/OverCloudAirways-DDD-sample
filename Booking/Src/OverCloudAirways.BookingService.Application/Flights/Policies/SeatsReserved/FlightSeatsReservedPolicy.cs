using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;

public class FlightSeatsReservedPolicy : DomainEventPolicy<FlightSeatsReservedDomainEvent>
{
    public FlightSeatsReservedPolicy(FlightSeatsReservedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

