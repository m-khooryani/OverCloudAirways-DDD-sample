using OverCloudAirways.PaymentService.Application.Products.Commands.Create;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.TestHelpers.Products;

public class CreateProductCommandBuilder
{
    private ProductId _productId = ProductId.New();
    private string _name = "First Class Ticket";
    private string _description = "A first-class ticket for flight XY123 from London to New York.";
    private decimal _price = 2000M;

    public CreateProductCommand Build()
    {
        return new CreateProductCommand(_productId, _name, _description, _price);
    }

    public CreateProductCommandBuilder SetProductId(ProductId productId)
    {
        _productId = productId;
        return this;
    }

    public CreateProductCommandBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public CreateProductCommandBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public CreateProductCommandBuilder SetPrice(decimal price)
    {
        _price = price;
        return this;
    }
}