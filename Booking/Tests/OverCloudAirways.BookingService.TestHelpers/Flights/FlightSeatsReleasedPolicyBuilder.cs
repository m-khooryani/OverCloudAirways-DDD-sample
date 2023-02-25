using OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReleased;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightSeatsReleasedPolicyBuilder
{
    private FlightSeatsReleasedDomainEvent _event = new FlightSeatsReleasedDomainEventBuilder().Build();

    public FlightSeatsReleasedPolicy Build()
    {
        return new FlightSeatsReleasedPolicy(_event);
    }

    public FlightSeatsReleasedPolicyBuilder SetEvent(FlightSeatsReleasedDomainEvent @event)
    {
        _event = @event;
        return this;
    }
}
