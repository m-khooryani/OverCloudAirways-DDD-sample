using OverCloudAirways.BookingService.Application.Aircrafts.Policies.Created;
using OverCloudAirways.BookingService.Domain.Aircrafts.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Aircrafts;

public class AircraftCreatedPolicyBuilder
{
    private AircraftCreatedDomainEvent _domainEvent = new AircraftCreatedDomainEventBuilder().Build();

    public AircraftCreatedPolicy Build()
    {
        return new AircraftCreatedPolicy(_domainEvent);
    }

    public AircraftCreatedPolicyBuilder SetDomainEvent(AircraftCreatedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
