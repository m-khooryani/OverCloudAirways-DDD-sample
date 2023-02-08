using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverCloudAirways.BookingService.Application.Airports.Queries.GetInfo;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using Xunit.Abstractions;
using Xunit;
using OverCloudAirways.BookingService.IntegrationTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Customers;
using OverCloudAirways.BookingService.Domain.Customers;
using OverCloudAirways.BookingService.Application.Customers.Queries.GetInfo;

namespace OverCloudAirways.BookingService.IntegrationTests.Customers;

[Collection("Booking")]
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
    }
}
