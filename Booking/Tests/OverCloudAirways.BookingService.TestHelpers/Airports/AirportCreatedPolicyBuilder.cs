using OverCloudAirways.BookingService.Application.Airports.Policies.Created;
using OverCloudAirways.BookingService.Domain.Airports.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Airports;

public class AirportCreatedPolicyBuilder
{
    private AirportCreatedDomainEvent _domainEvent = new AirportCreatedDomainEventBuilder().Build();

    public AirportCreatedPolicy Build()
    {
        return new AirportCreatedPolicy(_domainEvent);
    }

    public AirportCreatedPolicyBuilder SetDomainEvent(AirportCreatedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
