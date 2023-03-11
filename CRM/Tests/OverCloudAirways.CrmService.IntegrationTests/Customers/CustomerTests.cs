using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.CrmService.Application.Customers.Queries.GetInfo;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.IntegrationTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.CrmService.IntegrationTests.Customers;

[Collection("CRM")]
public class CustomerTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public CustomerTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task CreateCustomer_CustomerShouldBeCreated_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var customerId = CustomerId.New();

        // Create Customer 
        var createCustomerCommand = new CreateCustomerCommandBuilder()
            .SetCustomerId(customerId)
            .Build();
        await _invoker.CommandAsync(createCustomerCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Customer Query
        var query = new GetCustomerInfoQuery(customerId.Value);
        var customer = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal(customerId.Value, customer.Id);
        Assert.Equal(createCustomerCommand.FirstName, customer.FirstName);
        Assert.Equal(createCustomerCommand.LastName, customer.LastName);
        Assert.Equal(createCustomerCommand.PhoneNumber, customer.PhoneNumber);
        Assert.Equal(createCustomerCommand.Address, customer.Address);
        Assert.Equal(createCustomerCommand.DateOfBirth, customer.DateOfBirth);
        Assert.Equal(createCustomerCommand.Email, customer.Email);
        Assert.Equal(0M, customer.LoyaltyPoints);
    }
}
