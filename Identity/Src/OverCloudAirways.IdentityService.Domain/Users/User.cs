using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public string Name { get; private set; }

    private User()
    {
    }

    public static User Register(UserId id, string name)
    {
        var @event = new UserRegisteredDomainEvent(
            id,
            name);

        var user = new User();
        user.Apply(@event);

        return user;
    }

    protected void When(UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        Name = @event.Name;
    }
}
