using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.Customers.Events;

namespace OverCloudAirways.CrmService.Domain.Customers;

public class Customer : AggregateRoot<CustomerId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Address { get; private set; }
    public decimal LoyaltyPoints { get; private set; }

    private Customer()
    {
    }

    public static Customer Create(
        CustomerId id,
        string firstName,
        string lastName,
        string email,
        DateOnly dateOfBirth,
        string phoneNumber,
        string address)
    {
        var @event = new CustomerCreatedDomainEvent(
            id,
            firstName,
            lastName,
            email,
            dateOfBirth,
            phoneNumber,
            address);

        var customer = new Customer();
        customer.Apply(@event);

        return customer;
    }

    protected void When(CustomerCreatedDomainEvent @event)
    {
        Id = @event.CustomerId;
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        Email = @event.Email;
        DateOfBirth = @event.DateOfBirth;
        PhoneNumber = @event.PhoneNumber;
        Address = @event.Address;
        LoyaltyPoints = 0M;
    }
}
