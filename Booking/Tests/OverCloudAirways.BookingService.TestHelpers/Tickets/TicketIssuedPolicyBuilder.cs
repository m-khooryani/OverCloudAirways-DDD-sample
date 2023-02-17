using OverCloudAirways.BookingService.Domain.Tickets.Events;
using OverCloudAirways.BookingService.Application.Tickets.Policies.Issued;

namespace OverCloudAirways.BookingService.TestHelpers.Tickets;

public class TicketIssuedPolicyBuilder
{
    private TicketIssuedDomainEvent _domainEvent = new TicketIssuedDomainEventBuilder().Build();

    public TicketIssuedPolicy Build()
    {
        return new TicketIssuedPolicy(_domainEvent);
    }

    public TicketIssuedPolicyBuilder SetDomainEvent(TicketIssuedDomainEvent domainEvent)
    {
        _domainEvent = domainEvent;
        return this;
    }
}