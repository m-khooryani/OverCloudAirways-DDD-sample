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

    public void Update(
        string name,
        string description,
        decimal price)
    {
        var @event = new ProductUpdatedDomainEvent(Id, name, description, price);
        Apply(@event);
    }

    public void Disable()
    {
        var @event = new ProductDisabledDomainEvent(Id);
        Apply(@event);
    }

    protected void When(ProductCreatedDomainEvent @event)
    {
        Id = @event.ProductId;
        Name = @event.Name;
        Description = @event.Description;
        Price = @event.Price;
        IsEnabled = true;
    }

    protected void When(ProductUpdatedDomainEvent @event)
    {
        Name = @event.Name;
        Description = @event.Description;
        Price = @event.Price;
    }

    protected void When(ProductDisabledDomainEvent _)
    {
        IsEnabled = false;
    }
}
