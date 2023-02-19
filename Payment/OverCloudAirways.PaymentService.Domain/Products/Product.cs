using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Products.Events;

namespace OverCloudAirways.PaymentService.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsEnabled { get; private set; }

    private Product()
    {
    }

    public static Product Create(
        ProductId productId, 
        string name, 
        string description,
        decimal price)
    {
        var @event = new ProductCreatedDomainEvent(productId, name, description, price);

        var product = new Product();
        product.Apply(@event);

        return product;
    }

    protected void When(ProductCreatedDomainEvent @event)
    {
        Id = @event.ProductId;
        Name = @event.Name;
        Description = @event.Description;
        Price = @event.Price;
        IsEnabled = true;
    }
}
