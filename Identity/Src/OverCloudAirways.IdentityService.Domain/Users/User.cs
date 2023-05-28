using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.IdentityService.Domain.Users.Events;

namespace OverCloudAirways.IdentityService.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public UserType UserType { get; private set; }
    public string Email { get; private set; }
    public string GivenName { get; private set; }
    public string Surname { get; private set; }
    public string Address { get; private set; }

    private User()
    {
    }

    public static User RegisterCustomer(
        UserId id,
        string email,
        string givenName,
        string surname,
        string address)
    {
        var @event = new CustomerUserRegisteredDomainEvent(
            id,
            email,
            givenName,
            surname,
            address);

        var user = new User();
        user.Apply(@event);

        return user;
    }

    public static User RegisterAsync(
        UserId id,
        UserType userType,
        string email,
        string givenName,
        string surname,
        string address)
    {
        // TODO: Check is not duplicate
        var @event = new UserRegisteredDomainEvent(
            id,
            userType,
            email,
            givenName,
            surname,
            address);

        var user = new User();
        user.Apply(@event);

        return user;
    }

    protected void When(CustomerUserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        UserType = UserType.Customer;
        Email = @event.Email;
        GivenName = @event.GivenName;
        Surname = @event.Surname;
        Address = @event.Address;
    }

    protected void When(UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        UserType = @event.UserType;
        Email = @event.Email;
        GivenName = @event.GivenName;
        Surname = @event.Surname;
        Address = @event.Address;
    }
}
