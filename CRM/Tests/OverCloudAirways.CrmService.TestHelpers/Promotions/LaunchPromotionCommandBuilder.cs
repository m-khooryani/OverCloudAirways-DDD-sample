using OverCloudAirways.CrmService.Application.Promotions.Commands.Launch;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.Promotions;

namespace OverCloudAirways.CrmService.TestHelpers.Promotions;

public class LaunchPromotionCommandBuilder
{
    private PromotionId _promotionId = PromotionId.New();
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;
    private string _description = "New year discount";
    private CustomerId? _customerId = CustomerId.New();

    public LaunchPromotionCommand Build()
    {
        return new LaunchPromotionCommand(
            _promotionId,
            _discountPercentage,
            _description,
            _customerId);
    }

    public LaunchPromotionCommandBuilder SetPromotionId(PromotionId promotionId)
    {
        _promotionId = promotionId;
        return this;
    }

    public LaunchPromotionCommandBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }

    public LaunchPromotionCommandBuilder SetDescription(string description)
    {
        _description = description;
        return this;
    }

    public LaunchPromotionCommandBuilder SetCustomerId(CustomerId? customerId)
    {
        _customerId = customerId;
        return this;
    }
}