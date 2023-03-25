using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Orders;

public abstract class OrderStatus : Enumeration
{
    public static readonly OrderStatus None = new NoneOrderStatus();
    public static readonly OrderStatus Pending = new PendingOrderStatus();
    public static readonly OrderStatus Failed = new FailedOrderStatus();
    public static readonly OrderStatus Confirmed = new ConfirmedOrderStatus();
    public static readonly OrderStatus Expired = new ExpiredOrderStatus();
    public static readonly OrderStatus Canceled = new CanceledOrderStatus();

    private OrderStatus(int value, string name) : base(value, name)
    {
    }

    private class NoneOrderStatus : OrderStatus
    {
        public NoneOrderStatus() : base(0, "None")
        {
        }
    }

    private class PendingOrderStatus : OrderStatus
    {
        public PendingOrderStatus() : base(1, "Pending")
        {
        }
    }

    private class FailedOrderStatus : OrderStatus
    {
        public FailedOrderStatus() : base(2, "Failed")
        {
        }
    }

    private class ConfirmedOrderStatus : OrderStatus
    {
        public ConfirmedOrderStatus() : base(3, "Confirmed")
        {
        }
    }

    private class ExpiredOrderStatus : OrderStatus
    {
        public ExpiredOrderStatus() : base(4, "Expired")
        {
        }
    }

    private class CanceledOrderStatus : OrderStatus
    {
        public CanceledOrderStatus() : base(5, "Canceled")
        {
        }
    }
}
