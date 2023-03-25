using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.PaymentService.Domain.Orders.Rules;

internal class OnlyPendingOrdersCanBeModifiedRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public OnlyPendingOrdersCanBeModifiedRule(OrderStatus status)
    {
        _status = status;
    }

    public string TranslationKey => "Only_Pending_Orders_Can_Be_Modified";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(_status == OrderStatus.Pending);
    }
}
