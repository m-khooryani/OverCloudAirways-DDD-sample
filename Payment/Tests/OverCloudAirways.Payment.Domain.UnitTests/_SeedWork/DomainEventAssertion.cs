using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

namespace OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;

public class DomainEventAssertion<T> where T : DomainEvent
{
    private readonly T _domainEvent;

    public DomainEventAssertion(T domainEvent)
    {
        _domainEvent = domainEvent;
    }

    public void WithPayload(Action<T> assertPayload)
    {
        assertPayload(_domainEvent);
    }
}