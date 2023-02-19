using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.PaymentService.Application.Products.Queries.GetInfo;
using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.IntegrationTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests.Products;

[Collection("Payment")]
public class ProductTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public ProductTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task CreateProduct_ProductShouldBeCreated_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var productId = ProductId.New();

        // Create Product 
        var createProductCommand = new CreateProductCommandBuilder()
            .SetProductId(productId)
            .Build();
        await _invoker.CommandAsync(createProductCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // Product Query
        var query = new GetProductInfoQuery(productId.Value);
        var product = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(productId.Value, product.Id);
        Assert.Equal(createProductCommand.Name, product.Name);
        Assert.Equal(createProductCommand.Description, product.Description);
        Assert.Equal(createProductCommand.Price, product.Price);
        Assert.True(product.IsEnabled);
    }
}
