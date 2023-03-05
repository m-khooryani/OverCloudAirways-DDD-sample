using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Invoices;

public abstract class InvoiceStatus : Enumeration
{
    public static readonly InvoiceStatus None = new NoneInvoiceStatus();
    public static readonly InvoiceStatus Pending = new PendingInvoiceStatus();
    public static readonly InvoiceStatus Accepted = new AcceptedInvoiceStatus();

    private InvoiceStatus(int value, string name) : base(value, name)
    {
    }

    private class NoneInvoiceStatus : InvoiceStatus
    {
        public NoneInvoiceStatus() : base(0, "None")
        {
        }
    }

    private class PendingInvoiceStatus : InvoiceStatus
    {
        public PendingInvoiceStatus() : base(1, "Pending")
        {
        }
    }

    private class AcceptedInvoiceStatus : InvoiceStatus
    {
        public AcceptedInvoiceStatus() : base(2, "Accepted")
        {
        }
    }
}
