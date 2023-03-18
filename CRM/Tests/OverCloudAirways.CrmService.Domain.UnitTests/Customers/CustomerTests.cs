using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Customers.Events;
using OverCloudAirways.CrmService.Domain.UnitTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using Xunit;

namespace OverCloudAirways.CrmService.Domain.UnitTests.Customers;

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
        Assert.Equal(0M, customer.LoyaltyPoints);
        AssertPublishedDomainEvent<CustomerCreatedDomainEvent>(customer);
    }

    [Fact]
    public void CollectCustomerLoyaltyPoints_Given_Valid_Input_Should_Successfully_Collect_Customer_LoyaltyPoints_And_Publish_Event()
    {
        // Arrange
        var loyaltyPoints = 100M;
        var customerId = CustomerId.New();
        var customer = new CustomerBuilder()
            .SetCustomerId(customerId)
            .Build();

        // Act
        customer.CollectLoyaltyPoints(loyaltyPoints);

        // Assert
        Assert.Equal(customerId, customer.Id);
        Assert.Equal(loyaltyPoints, customer.LoyaltyPoints);
        AssertPublishedDomainEvent<CustomerLoyaltyPointsCollectedDomainEvent>(customer);
    }

    [Fact]
    public void ResetCustomerLoyaltyPoints_Given_Valid_Input_Should_Successfully_Reset_Customer_LoyaltyPoints_And_Publish_Event()
    {
        // Arrange
        var customerId = CustomerId.New();
        var customer = new CustomerBuilder()
            .SetCustomerId(customerId)
            .Build();

        // Act
        customer.CollectLoyaltyPoints(100M);
        customer.ResetLoyaltyPoints();

        // Assert
        Assert.Equal(customerId, customer.Id);
        Assert.Equal(0M, customer.LoyaltyPoints);
        AssertPublishedDomainEvent<CustomerLoyaltyPointsResetDomainEvent>(customer);
    }
}
