using OverCloudAirways.BookingService.Application.FlightBookings.Policies.Reserved;
using OverCloudAirways.BookingService.Domain.FlightBookings.Events;

namespace OverCloudAirways.BookingService.TestHelpers.FlightBookings;

public class FlightBookingReservedPolicyBuilder
{
    private FlightBookingReservedDomainEvent _domainEvent = new FlightBookingReservedDomainEventBuilder().Build();

    public FlightBookingReservedPolicy Build()
    {
        return new FlightBookingReservedPolicy(_domainEvent);
    }

    public FlightBookingReservedPolicyBuilder SetDomainEvent(FlightBookingReservedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}
