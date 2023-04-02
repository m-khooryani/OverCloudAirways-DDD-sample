using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.CrmService.Application.Purchases.Queries.GetInfo;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.Purchases;
using OverCloudAirways.CrmService.IntegrationTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using OverCloudAirways.CrmService.TestHelpers.Purchases;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.CrmService.IntegrationTests.Purchases;

[Collection("CRM")]
public class PurchaseTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public PurchaseTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task MakePurchase_PurchaseShouldBeMade_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var date = DateTimeOffset.UtcNow;
        Clock.SetCustomDate(date);
        var customerId = CustomerId.New();
        var purchaseId = PurchaseId.New();

        // Create Customer 
        var createCustomerCommand = new CreateCustomerCommandBuilder()
            .SetCustomerId(customerId)
            .Build();
        await _invoker.CommandAsync(createCustomerCommand);

        // Make Purchase
        var makePurchaseCommand = new MakePurchaseCommandBuilder()
            .SetPurchaseId(purchaseId)
            .SetCustomerId(customerId)
            .Build();
        await _invoker.CommandAsync(makePurchaseCommand);

        // Process Outbox messages
        await _testFixture.ProcessOutboxMessagesAsync();

        // Purchase Query
        var query = new GetPurchaseInfoQuery(purchaseId, customerId);
        var purchase = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(purchase);
        Assert.Equal(purchaseId.Value, purchase.PurchaseId);
        Assert.Equal(createCustomerCommand.CustomerId.Value, purchase.CustomerId);
        Assert.Equal(createCustomerCommand.FirstName, purchase.CustomerFirstName);
        Assert.Equal(createCustomerCommand.LastName, purchase.CustomerLastName);
        Assert.Equal(makePurchaseCommand.Amount, purchase.Amount);
        Assert.Equal(date, purchase.Date);
    }
}
