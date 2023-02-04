using OverCloudAirways.BookingService.Application.Flights.Policies.Canceled;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightCanceledPolicyBuilder
{
    private FlightCanceledDomainEvent _domainEvent = new FlightCanceledDomainEventBuilder().Build();

    public FlightCanceledPolicy Build()
    {
        return new FlightCanceledPolicy(_domainEvent);
    }

    public FlightCanceledPolicyBuilder SetDomainEvent(FlightCanceledDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
