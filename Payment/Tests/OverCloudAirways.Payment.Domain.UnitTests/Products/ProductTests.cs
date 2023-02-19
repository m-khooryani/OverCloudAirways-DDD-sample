using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Products.Events;
using OverCloudAirways.PaymentService.Domain.UnitTests._SeedWork;
using OverCloudAirways.PaymentService.TestHelpers.Products;
using Xunit;

namespace OverCloudAirways.PaymentService.Domain.UnitTests.Products;

public class ProductTests : Test
{
    [Fact]
    public void CreateProduct_Given_Valid_Input_Should_Successfully_Create_Product_And_Publish_Event()
    {
        // Arrange
        var productId = ProductId.New();
        var name = "First Class Ticket";
        var description = "A first-class ticket for flight XY123 from London to New York.";
        var price = 2000M;
        var productBuilder = new ProductBuilder()
            .SetProductId(productId)
            .SetName(name)
            .SetDescription(description)
            .SetPrice(price);

        // Act
        var product = productBuilder.Build();

        // Assert
        Assert.Equal(productId, product.Id);
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.True(product.IsEnabled);
        AssertPublishedDomainEvent<ProductCreatedDomainEvent>(product);
    }

    [Fact]
    public void UpdateProduct_Given_Valid_Input_Should_Successfully_Update_Product_And_Publish_Event()
    {
        // Arrange
        var productId = ProductId.New();
        var name = "Economy Class Ticket";
        var description = "A economy-class ticket for flight XY123 from London to New York.";
        var price = 1500M;
        var product = new ProductBuilder()
            .SetProductId(productId)
            .Build();

        // Act
        product.Update(name, description, price);

        // Assert
        Assert.Equal(productId, product.Id);
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.True(product.IsEnabled);
        AssertPublishedDomainEvent<ProductUpdatedDomainEvent>(product);
    }
}
