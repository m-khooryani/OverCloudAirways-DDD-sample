using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Domain.Customers.Events;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Customers;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Customers;

public class CustomerTests : Test
{
    [Fact]
    public void CreateCustomer_Given_Valid_Input_Should_Successfully_Create_Customer_And_Publish_Event()
    {
        // Arrange
        var customerId = CustomerId.New();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = Clock.Today;  
        var phoneNumber = "1234567890";
        var address = "1234 Main St";
        var customerBuilder = new CustomerBuilder()
            .SetCustomerId(customerId)
            .SetFirstName(firstName)
            .SetLastName(lastName)
            .SetEmail(email)
            .SetDateOfBirth(dateOfBirth)
            .SetPhoneNumber(phoneNumber)
            .SetAddress(address);

        // Act
        var customer = customerBuilder.Build();

        // Assert
        Assert.Equal(customerId, customer.Id); 
        Assert.Equal(firstName, customer.FirstName);
        Assert.Equal(lastName, customer.LastName);
        Assert.Equal(email, customer.Email);
        Assert.Equal(dateOfBirth, customer.DateOfBirth);
        Assert.Equal(phoneNumber, customer.PhoneNumber);
        Assert.Equal(address, customer.Address);
        AssertPublishedDomainEvent<CustomerCreatedDomainEvent>(customer);
    }
}
