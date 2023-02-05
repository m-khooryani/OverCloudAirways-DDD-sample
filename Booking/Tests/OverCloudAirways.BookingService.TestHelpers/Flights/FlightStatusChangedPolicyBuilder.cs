using OverCloudAirways.BookingService.Application.Flights.Policies.StatusChanged;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightStatusChangedPolicyBuilder
{
    private FlightStatusChangedDomainEvent _domainEvent = new FlightStatusChangedDomainEventBuilder().Build();

    public FlightStatusChangedPolicy Build()
    {
        return new FlightStatusChangedPolicy(_domainEvent);
    }

    public FlightStatusChangedPolicyBuilder SetDomainEvent(FlightStatusChangedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}