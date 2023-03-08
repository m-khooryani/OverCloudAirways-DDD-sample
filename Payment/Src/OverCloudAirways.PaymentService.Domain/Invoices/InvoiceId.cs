using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Invoices;

public class InvoiceId : TypedId<Guid>
{
    public InvoiceId(Guid value) : base(value)
    {
    }

    public static InvoiceId New()
    {
        return new InvoiceId(Guid.NewGuid());
    }
}