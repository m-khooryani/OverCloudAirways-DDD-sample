using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Orders;
using OverCloudAirways.PaymentService.Domain.Products;

namespace OverCloudAirways.PaymentService.Domain.Invoices;

public class InvoiceItem : ValueObject
{
    public string Description { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }
    public decimal TotalPrice { get; }

    private InvoiceItem()
    {
    }

    private InvoiceItem(string description, decimal unitPrice, int quantity)
    {
        Description = description;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = quantity * unitPrice;
    }

    public static InvoiceItem Of(string description, decimal unitPrice, int quantity)
    {
        return new InvoiceItem(description, unitPrice, quantity);
    }

    internal static InvoiceItem Of(PricedOrderItem item, Product product)
    {
        return Of(product.Name, product.Price, item.Quantity);
    }
}
