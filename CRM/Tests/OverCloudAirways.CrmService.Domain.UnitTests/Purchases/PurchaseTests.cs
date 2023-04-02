using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;
using OverCloudAirways.CrmService.Domain.Purchases.Events;
using OverCloudAirways.CrmService.Domain.UnitTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Purchases;
using Xunit;

namespace OverCloudAirways.CrmService.Domain.UnitTests.Purchases;

public class PurchaseTests : Test
{
    [Fact]
    public void MakePurchase_Given_Valid_Input_Should_Successfully_Make_Purchase_And_Publish_Event()
    {
        // Arrange
        var purchaseId = PurchaseId.New();
        var customerId = CustomerId.New();
        var amount = 100M;
        var date = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(date);

        // Act
        var purchase = new PurchaseBuilder()
            .SetPurchaseId(purchaseId)
            .SetCustomerId(customerId)
            .SetAmount(amount)
            .Build();

        // Assert
        Assert.Equal(purchaseId, purchase.Id);
        Assert.Equal(customerId, purchase.CustomerId);
        Assert.Equal(amount, purchase.Amount);
        Assert.Equal(date, purchase.Date);
        AssertPublishedDomainEvent<PurchaseMadeDomainEvent>(purchase);
    }
}
