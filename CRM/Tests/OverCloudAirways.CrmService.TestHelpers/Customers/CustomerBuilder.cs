using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CustomerBuilder
{
    private CustomerId _customerId = CustomerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private DateOnly _dateOfBirth = Clock.Today;
    private string _phoneNumber = "1234567890";
    private string _address = "1234 Main St";

    public Customer Build()
    {
        return Customer.Create(_customerId, _firstName, _lastName, _email, _dateOfBirth, _phoneNumber, _address);
    }

    public CustomerBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public CustomerBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CustomerBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CustomerBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public CustomerBuilder SetDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public CustomerBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public CustomerBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }
}
