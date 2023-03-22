using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.TestHelpers.Promotions;

public class PromotionBuilder
{
    private IDiscountCodeGenerator _discountCodeGenerator;
    private PromotionId _promotionId = PromotionId.New();
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;
    private string _description = "New year discount";
    private CustomerId _customerId = CustomerId.New();

    public Promotion Build()
    {
        return Promotion.Launch(
            _discountCodeGenerator,
            _promotionId,
            _discountPercentage,
            _description,
            _customerId);
    }

    public PromotionBuilder SetDiscountCodeGenerator(IDiscountCodeGenerator discountCodeGenerator)
    {
        _discountCodeGenerator = discountCodeGenerator;
        return this;
    }

    public PromotionBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public PromotionBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public PromotionBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public PromotionBuilder SetCustomerId(CustomerId customerId)
    {
        _customerId = customerId;
        return this;
    }
}