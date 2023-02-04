using OverCloudAirways.BookingService.Application.Flights.Policies.CapacityChanged;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightCapacityChangedPolicyBuilder
{
    private FlightCapacityChangedDomainEvent _domainEvent = new FlightCapacityChangedDomainEventBuilder().Build();

    public FlightCapacityChangedPolicy Build()
    {
        return new FlightCapacityChangedPolicy(_domainEvent);
    }

    public FlightCapacityChangedPolicyBuilder SetDomainEvent(FlightCapacityChangedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
