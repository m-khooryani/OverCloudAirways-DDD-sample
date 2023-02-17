using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BuildingBlocks.Application.DomainEventPolicies;

namespace OverCloudAirways.BookingService.Application.Tickets.Policies.Issued;

public class TicketIssuedPolicy : DomainEventPolicy<TicketIssuedDomainEvent>
{
    public TicketIssuedPolicy(TicketIssuedDomainEvent domainEvent) : base(domainEvent)
    {
    }
}
