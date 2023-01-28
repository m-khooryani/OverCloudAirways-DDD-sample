using OverCloudAirways.BookingService.Domain.Aircrafts.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Aircrafts.Policies.Created;

public class AircraftCreatedPolicy : DomainEventPolicy<AircraftCreatedDomainEvent>
{
    public AircraftCreatedPolicy(AircraftCreatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

