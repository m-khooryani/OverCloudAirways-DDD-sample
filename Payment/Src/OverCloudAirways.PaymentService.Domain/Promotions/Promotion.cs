using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.PaymentService.Domain.Buyers;
using OverCloudAirways.PaymentService.Domain.Promotions.Events;

namespace OverCloudAirways.PaymentService.Domain.Promotions;

public class Promotion : AggregateRoot<PromotionId>
{
    public string DiscountCode { get; private set; }
    public Percentage DiscountPercentage { get; private set; }
    public string? Description { get; private set; }
    public BuyerId? BuyerId { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }

    private Promotion()
    {
    }

    public static Promotion Launch(
        PromotionId id,
        string discountCode,
        Percentage discountPercentage,
        string? description,
        BuyerId? buyerId,
        DateTimeOffset expirationDate)
    {
        var @event = new PromotionLaunchedDomainEvent(
            id,
            discountCode,
            discountPercentage,
            description,
            buyerId,
            expirationDate);

        var promotion = new Promotion();
        promotion.Apply(@event);

        return promotion;
    }

    public void Extend(int months)
    {
        var @event = new PromotionExtendedDomainEvent(Id, months);
        Apply(@event);
    }

    protected void When(PromotionLaunchedDomainEvent @event)
    {
        Id = @event.PromotionId;
        DiscountCode = @event.DiscountCode;
        DiscountPercentage = @event.DiscountPercentage;
        Description = @event.Description;
        BuyerId = @event.BuyerId;
        ExpirationDate = @event.ExpirationDate;
    }

    protected void When(PromotionExtendedDomainEvent @event)
    {
        ExpirationDate = ExpirationDate.AddMonths(@event.Months);
    }
}
