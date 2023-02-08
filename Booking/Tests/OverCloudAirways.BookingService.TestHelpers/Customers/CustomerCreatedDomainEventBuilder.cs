using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Customers.Events;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;

namespace OverCloudAirways.BookingService.TestHelpers.Customers;

public class CustomerCreatedDomainEventBuilder
{
    private CustomerId _customerId = CustomerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private DateOnly _dateOfBirth = Clock.Today;
    private string _phoneNumber = "1234567890";
    private string _address = "1234 Main St";

    public CustomerCreatedDomainEvent Build()
    {
        return new CustomerCreatedDomainEvent(_customerId, _firstName, _lastName, _email, _dateOfBirth, _phoneNumber, _address);
    }

    public CustomerCreatedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public CustomerCreatedDomainEventBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }
}
