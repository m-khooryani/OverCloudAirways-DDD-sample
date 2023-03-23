using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.Domain.Promotions;

public class Promotion : AggregateRoot<PromotionId>
{
    public string DiscountCode { get; private set; }
    public Percentage DiscountPercentage { get; private set; }
    public string? Description { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }

    private Promotion()
    {
    }

    public static Promotion Launch(
        IDiscountCodeGenerator discountCodeGenerator,
        PromotionId id,
        Percentage discountPercentage,
        string? description,
        CustomerId? customerId)
    {
        var @event = new PromotionLaunchedDomainEvent(
            id, 
            discountCodeGenerator.Generate(), 
            discountPercentage, 
            description, 
            customerId, 
            Clock.Now.AddYears(1));

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
        CustomerId = @event.CustomerId;
        ExpirationDate = @event.ExpirationDate;
    }

    protected void When(PromotionExtendedDomainEvent @event)
    {
        ExpirationDate = ExpirationDate.AddMonths(@event.Months);
    }
}