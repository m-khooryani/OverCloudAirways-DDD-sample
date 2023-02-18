using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Buyers.Events;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Buyers;

public class BuyerTests : Test
{
    [Fact]
    public void RegisterBuyer_Given_Valid_Input_Should_Successfully_Register_Buyer_And_Publish_Event()
    {
        // Arrange
        var buyerId = BuyerId.New();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var phoneNumber = "1234567890";
        var buyerBuilder = new BuyerBuilder()
            .SetBuyerId(buyerId)
            .SetFirstName(firstName)
            .SetLastName(lastName)
            .SetEmail(email)
            .SetPhoneNumber(phoneNumber);

        // Act
        var buyer = buyerBuilder.Build();

        // Assert
        Assert.Equal(buyerId, buyer.Id);
        Assert.Equal(firstName, buyer.FirstName);
        Assert.Equal(lastName, buyer.LastName);
        Assert.Equal(email, buyer.Email);
        Assert.Equal(phoneNumber, buyer.PhoneNumber);
        AssertPublishedDomainEvent<BuyerRegisteredDomainEvent>(buyer);
    }

    [Fact]
    public void Refund_Given_Valid_Input_Should_Successfully_Refund_Buyer_Balance_And_Publish_Event()
    {
        // Arrange
        var buyer = new BuyerBuilder()
            .Build();

        // Act
        buyer.Refund(20M);
        buyer.Refund(30M);

        // Assert
        Assert.Equal(50, buyer.Balance);
        AssertPublishedDomainEvent<BuyerBalanceRefundedDomainEvent>(buyer, 2);
    }
}
