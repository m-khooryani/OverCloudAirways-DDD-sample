using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;

namespace OverCloudAirways.CrmService.TestHelpers.LoyaltyPrograms;

public class LoyaltyProgramPlannedDomainEventBuilder 
{
    private LoyaltyProgramId _loyaltyProgramId = LoyaltyProgramId.New();
    private string _name = "Gold Rewards";
    private decimal _purchaseRequirements = 10_000M;
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;

    public LoyaltyProgramPlannedDomainEvent Build()
    {
        return new LoyaltyProgramPlannedDomainEvent(_loyaltyProgramId, _name, _purchaseRequirements, _discountPercentage);
    }

    public LoyaltyProgramPlannedDomainEventBuilder SetLoyaltyProgramId(LoyaltyProgramId loyaltyProgramId)
    {
        _loyaltyProgramId = loyaltyProgramId;
        return this;
    }

    public LoyaltyProgramPlannedDomainEventBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public LoyaltyProgramPlannedDomainEventBuilder SetPurchaseRequirements(decimal purchaseRequirements)
    {
        _purchaseRequirements = purchaseRequirements;
        return this;
    }

    public LoyaltyProgramPlannedDomainEventBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }
}
