using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class ProductBuilder
{
    private ProductId _productId = ProductId.New();
    private string _name = "First Class Ticket";
    private string _description = "A first-class ticket for flight XY123 from London to New York.";
    private decimal _price = 2000M;

    public Product Build()
    {
        return Product.Create(_productId, _name, _description, _price);
    }

    public ProductBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }

    public ProductBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ProductBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public ProductBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }
}
