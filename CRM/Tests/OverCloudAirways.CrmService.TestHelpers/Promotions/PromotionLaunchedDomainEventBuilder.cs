using OverCloudAirways.BuildingBlocks.Domain.Utilities;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;
using OverCloudAirways.CrmService.Domain.Promotions.Events;

namespace OverCloudAirways.CrmService.TestHelpers.Promotions;

public class PromotionLaunchedDomainEventBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private string _discountCode = "OA_10023";
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;
    private string _description = "New year discount";
    private CustomerId _customerId = CustomerId.New();
    private DateTimeOffset _expirationDate = Clock.Now.AddYears(1);

    public PromotionLaunchedDomainEvent Build()
    {
        return new PromotionLaunchedDomainEvent(
            _promotionId,
            _discountCode,
            _discountPercentage,
            _description,
            _customerId,
            _expirationDate);
    }

    public PromotionLaunchedDomainEventBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDiscountCode(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }

    public PromotionLaunchedDomainEventBuilder SetExpirationDate(DateTimeOffset expirationDate)
    {
        _expirationDate = expirationDate;
        return this;
    }
}
