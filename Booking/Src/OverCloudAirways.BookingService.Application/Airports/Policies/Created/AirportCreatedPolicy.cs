using OverCloudAirways.BookingService.Domain.Airports.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Airports.Policies.Created;

public class AirportCreatedPolicy : DomainEventPolicy<AirportCreatedDomainEvent>
{
    public AirportCreatedPolicy(AirportCreatedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
