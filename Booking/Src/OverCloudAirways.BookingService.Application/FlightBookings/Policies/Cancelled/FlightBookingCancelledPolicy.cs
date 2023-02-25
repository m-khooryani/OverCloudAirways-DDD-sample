using OverCloudAirways.BookingService.Domain.FlightBookings.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.FlightBookings.Policies.Cancelled;

public class FlightBookingCancelledPolicy : DomainEventPolicy<FlightBookingCancelledDomainEvent>
{
    public FlightBookingCancelledPolicy(FlightBookingCancelledDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
