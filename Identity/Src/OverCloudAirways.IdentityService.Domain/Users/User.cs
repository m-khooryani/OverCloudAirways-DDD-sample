using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public UserType UserType { get; private set; }
    public string GivenName { get; private set; }
    public string Surname { get; private set; }

    private User()
    {
    }

    public static User Register(
        UserId id,
        UserType userType,
        string givenName,
        string surname)
    {
        var @event = new UserRegisteredDomainEvent(
            id,
            userType,
            givenName,
            surname);

        var user = new User();
        user.Apply(@event);

        return user;
    }

    protected void When(UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        UserType = @event.UserType;
        GivenName = @event.GivenName;
    }
}
