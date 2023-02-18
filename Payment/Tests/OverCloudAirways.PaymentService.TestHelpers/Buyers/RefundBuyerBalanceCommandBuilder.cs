using OverCloudAirways.PaymentService.Application.Buyers.Commands.Refund;
using OverCloudAirways.PaymentService.Domain.Buyers;

namespace OverCloudAirways.PaymentService.TestHelpers.Buyers;

public class RefundBuyerBalanceCommandBuilder
{
    private BuyerId _buyerId = BuyerId.New();
    private decimal _amount = 2M;

    public RefundBuyerBalanceCommand Build()
    {
        return new RefundBuyerBalanceCommand(_buyerId, _amount);
    }

    public RefundBuyerBalanceCommandBuilder SetBuyerId(BuyerId buyerId)
    { 
        _buyerId = buyerId; 
        return this; 
    }

    public RefundBuyerBalanceCommandBuilder SetAmount(decimal amount)
    { 
        _amount = amount;
        return this; 
    }
}
