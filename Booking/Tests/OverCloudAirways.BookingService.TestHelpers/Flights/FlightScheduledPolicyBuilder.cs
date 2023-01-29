using OverCloudAirways.BookingService.Application.Flights.Policies.Scheduled;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightScheduledPolicyBuilder
{
    private FlightScheduledDomainEvent _domainEvent = new FlightScheduledDomainEventBuilder().Build();

    public FlightScheduledPolicy Build()
    {
        return new FlightScheduledPolicy(_domainEvent);
    }

    public FlightScheduledPolicyBuilder SetDomainEvent(FlightScheduledDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
