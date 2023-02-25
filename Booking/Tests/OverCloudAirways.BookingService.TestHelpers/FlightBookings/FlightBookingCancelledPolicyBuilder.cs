using OverCloudAirways.BookingService.Application.FlightBookings.Policies.Cancelled;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;

namespace OverCloudAirways.BookingService.TestHelpers.FlightBookings;

public class FlightBookingCancelledPolicyBuilder
{
    private FlightBookingCancelledDomainEvent _domainEvent = new FlightBookingCancelledDomainEventBuilder().Build();

    public FlightBookingCancelledPolicy Build()
    {
        return new (_domainEvent);
    }

    public FlightBookingCancelledPolicyBuilder SetDomainEvent(FlightBookingCancelledDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
