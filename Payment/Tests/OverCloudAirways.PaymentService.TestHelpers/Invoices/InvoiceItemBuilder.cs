using OverCloudAirways.PaymentService.Domain.Invoices;

namespace OverCloudAirways.PaymentService.TestHelpers.Invoices;

public class InvoiceItemBuilder
{
    private string _description = "Product A";
    private decimal _unitPrice = 100M;
    private int _quantity = 1;

    public InvoiceItem Build()
    {
        return InvoiceItem.Of(_description, _unitPrice, _quantity);
    }

    public InvoiceItemBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public InvoiceItemBuilder SetUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public InvoiceItemBuilder SetQuantity(int quantity)
    {
        _quantity = quantity;
        return this;
    }
}