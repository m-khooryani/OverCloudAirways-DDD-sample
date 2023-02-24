using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Policies.Reserved;

public class FlightBookingReservedPolicy : DomainEventPolicy<FlightBookingReservedDomainEvent>
{
    public FlightBookingReservedPolicy(FlightBookingReservedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
