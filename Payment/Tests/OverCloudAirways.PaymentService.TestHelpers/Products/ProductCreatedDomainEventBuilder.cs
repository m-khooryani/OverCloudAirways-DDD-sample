using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductCreatedDomainEventBuilder
{
    private ProductId _productId = ProductId.New();
    private string _name = "First Class Ticket";
    private string _description = "A first-class ticket for flight XY123 from London to New York.";
    private decimal _price = 2000M;

    public ProductCreatedDomainEvent Build()
    {
        return new ProductCreatedDomainEvent(_productId, _name, _description, _price);
    }

    public ProductCreatedDomainEventBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }

    public ProductCreatedDomainEventBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ProductCreatedDomainEventBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public ProductCreatedDomainEventBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }
}
