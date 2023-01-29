using OverCloudAirways.BookingService.Application.Flights.Policies.PriceCharged;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightPriceChargedPolicyBuilder
{
    private FlightPriceChargedDomainEvent _domainEvent = new FlightPriceChargedDomainEventBuilder().Build();

    public FlightPriceChargedPolicy Build()
    {
        return new FlightPriceChargedPolicy(_domainEvent);
    }

    public FlightPriceChargedPolicyBuilder SetDomainEvent(FlightPriceChargedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
