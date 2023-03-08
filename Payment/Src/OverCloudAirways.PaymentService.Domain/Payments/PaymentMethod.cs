using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Payments;

public abstract class PaymentMethod : Enumeration
{
    public static readonly PaymentMethod None = new NonePaymentMethod();
    public static readonly PaymentMethod CreditCard = new CreditCardPaymentMethod();
    public static readonly PaymentMethod DebitCard = new DebitCardPaymentMethod();
    public static readonly PaymentMethod PayPal = new PayPalPaymentMethod();
    public static readonly PaymentMethod BankTransfer = new BankTransferPaymentMethod();
    public static readonly PaymentMethod Cash = new CashPaymentMethod();

    private PaymentMethod(int value, string name) : base(value, name)
    {
    }

    private class NonePaymentMethod : PaymentMethod
    {
        public NonePaymentMethod() : base(0, "None")
        {
        }
    }

    private class CreditCardPaymentMethod : PaymentMethod
    {
        public CreditCardPaymentMethod() : base(1, "CreditCard")
        {
        }
    }

    private class DebitCardPaymentMethod : PaymentMethod
    {
        public DebitCardPaymentMethod() : base(2, "DebitCard")
        {
        }
    }

    private class PayPalPaymentMethod : PaymentMethod
    {
        public PayPalPaymentMethod() : base(3, "PayPal")
        {
        }
    }

    private class BankTransferPaymentMethod : PaymentMethod
    {
        public BankTransferPaymentMethod() : base(4, "BankTransfer")
        {
        }
    }

    private class CashPaymentMethod : PaymentMethod
    {
        public CashPaymentMethod() : base(5, "Cash")
        {
        }
    }
}