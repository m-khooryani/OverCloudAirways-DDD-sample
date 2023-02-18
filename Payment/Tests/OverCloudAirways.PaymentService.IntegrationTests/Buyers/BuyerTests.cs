using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Buyers.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Buyers;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Buyers;

[Collection("Payment")]
public class BuyerTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public BuyerTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task RegisterBuyer_BuyerShouldBeRegistered_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var buyerId = BuyerId.New();

        // Create Customer 
        var createCustomerCommand = new RegisterBuyerCommandBuilder()
            .SetBuyerId(buyerId)
            .Build();
        await _invoker.CommandAsync(createCustomerCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Customer Query
        var query = new GetBuyerInfoQuery(buyerId.Value);
        var customer = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(buyerId.Value, customer.Id);
        Assert.Equal(createCustomerCommand.FirstName, customer.FirstName);
        Assert.Equal(createCustomerCommand.LastName, customer.LastName);
        Assert.Equal(createCustomerCommand.PhoneNumber, customer.PhoneNumber);
        Assert.Equal(createCustomerCommand.Email, customer.Email);
    }
}
