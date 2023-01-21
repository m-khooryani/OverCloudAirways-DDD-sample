namespace OverCloudAirways.BuildingBlocks.Domain.DomainEvents;

public interface IUpCastable<TEvent>
    where TEvent : DomainEvent
{
    TEvent UpCast();
}
