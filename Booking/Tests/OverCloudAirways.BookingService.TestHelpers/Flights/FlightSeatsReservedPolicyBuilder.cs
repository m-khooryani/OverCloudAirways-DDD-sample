using OverCloudAirways.BookingService.Application.Flights.Policies.SeatsReserved;
using OverCloudAirways.BookingService.Domain.Flights.Events;

namespace OverCloudAirways.BookingService.TestHelpers.Flights;

public class FlightSeatsReservedPolicyBuilder
{
    private FlightSeatsReservedDomainEvent _event = new FlightSeatsReservedDomainEventBuilder().Build();

    public FlightSeatsReservedPolicy Build()
    {
        return new FlightSeatsReservedPolicy(_event);
    }

    public FlightSeatsReservedPolicyBuilder SetEvent(FlightSeatsReservedDomainEvent @event)
    {
        _event = @event;
        return this;
    }
}
