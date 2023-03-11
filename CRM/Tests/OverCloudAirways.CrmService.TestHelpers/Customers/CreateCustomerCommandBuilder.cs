using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Application.Customers.Commands.Create;
using OverCloudAirways.CrmService.Domain.Customers;

namespace OverCloudAirways.CrmService.TestHelpers.Customers;

public class CreateCustomerCommandBuilder
{
    private CustomerId _customerId = CustomerId.New();
    private string _firstName = "John";
    private string _lastName = "Doe";
    private string _email = "john.doe@example.com";
    private DateOnly _dateOfBirth = Clock.Today;
    private string _phoneNumber = "1234567890";
    private string _address = "1234 Main St";

    public CreateCustomerCommand Build()
    {
        return new CreateCustomerCommand(_customerId, _firstName, _lastName, _email, _dateOfBirth, _phoneNumber, _address);
    }

    public CreateCustomerCommandBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public CreateCustomerCommandBuilder SetFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public CreateCustomerCommandBuilder SetLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public CreateCustomerCommandBuilder SetEmail(string email)
    {
        _email = email;
        return this;
    }

    public CreateCustomerCommandBuilder SetDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public CreateCustomerCommandBuilder SetPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public CreateCustomerCommandBuilder SetAddress(string address)
    {
        _address = address;
        return this;
    }
}