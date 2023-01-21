using OverCloudAirways.BuildingBlocks.Domain.DomainEvents;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public interface IAggregateRoot
{

}

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    where TKey : TypedId
{
    private readonly Queue<DomainEvent> _domainEvents;
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();
    public int Version { get; private set; }

    protected AggregateRoot()
    {
        _domainEvents = new Queue<DomainEvent>();
    }

    private void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Enqueue(domainEvent);
    }

    internal void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Invoke "When" method, then append the event to changes
    /// </summary>
    /// <param name="event"></param>
    protected void Apply(DomainEvent @event)
    {
        if (IsUpcastable(@event))
        {
            throw new Exception($"{@event.GetType().Name} is deprecated.");
        }
        DynamicInvoker.Invoke(this, "When", @event);
        AddDomainEvent(@event);
    }

    private static bool IsUpcastable(DomainEvent @event)
    {
        return @event.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUpCastable<>));
    }

    internal void Load(DomainEvent @event)
    {
        Apply(@event);
    }

    public void Load(IEnumerable<DomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            var latestVersion = domainEvent;

            while (IsUpcastable(latestVersion))
            {
                latestVersion = DynamicInvoker.Invoke(latestVersion, nameof(IUpCastable<DomainEvent>.UpCast)) as DomainEvent;
            }

            DynamicInvoker.Invoke(this, "When", latestVersion);
            Version++;
        }
        ClearDomainEvents();
    }
}
