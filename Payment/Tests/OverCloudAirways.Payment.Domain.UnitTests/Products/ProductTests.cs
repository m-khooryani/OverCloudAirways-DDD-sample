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
        var _name = "First Class Ticket";
        var _description = "A first-class ticket for flight XY123 from London to New York.";
        var _price = 2000M;
        var productBuilder = new ProductBuilder()
            .SetProductId(productId)
            .SetName(_name)
            .SetDescription(_description)
            .SetPrice(_price);

        // Act
        var product = productBuilder.Build();

        // Assert
        Assert.Equal(productId, product.Id);
        Assert.Equal(_name, product.Name);
        Assert.Equal(_description, product.Description);
        Assert.True(product.IsEnabled);
        AssertPublishedDomainEvent<ProductCreatedDomainEvent>(product);
    }
}
