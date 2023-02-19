using OverCloudAirways.PaymentService.Domain.Products;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductUpdatedDomainEventBuilder
{
    private ProductId _productId = ProductId.New();
    private string _name = "Econoy Class Ticket";
    private string _description = "A economy-class ticket for flight XY123 from London to New York.";
    private decimal _price = 1500M;

    public ProductUpdatedDomainEvent Build()
    {
        return new ProductUpdatedDomainEvent(_productId, _name, _description, _price);
    }

    public ProductUpdatedDomainEventBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }

    public ProductUpdatedDomainEventBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ProductUpdatedDomainEventBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public ProductUpdatedDomainEventBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }
}
