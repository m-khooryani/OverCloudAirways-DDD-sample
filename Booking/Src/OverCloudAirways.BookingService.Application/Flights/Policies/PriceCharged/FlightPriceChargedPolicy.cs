using OverCloudAirways.BookingService.Domain.Flights.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Flights.Policies.PriceCharged;

public class FlightPriceChargedPolicy : DomainEventPolicy<FlightPriceChargedDomainEvent>
{
    public FlightPriceChargedPolicy(FlightPriceChargedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}

