using OverCloudAirways.CrmService.Application.LoyaltyPrograms.Commands.Plan;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.TestHelpers.LoyaltyPrograms;

public class PlanLoyaltyProgramCommandBuilder
{
    private LoyaltyProgramId _loyaltyProgramId = LoyaltyProgramId.New();
    private string _name = "Gold Rewards";
    private decimal _purchaseRequirements = 10_000M;
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;

    public PlanLoyaltyProgramCommand Build()
    {
        return new PlanLoyaltyProgramCommand(_loyaltyProgramId, _name, _purchaseRequirements, _discountPercentage);
    }

    public PlanLoyaltyProgramCommandBuilder SetLoyaltyProgramId(LoyaltyProgramId loyaltyProgramId)
    {
        _loyaltyProgramId = loyaltyProgramId;
        return this;
    }

    public PlanLoyaltyProgramCommandBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public PlanLoyaltyProgramCommandBuilder SetPurchaseRequirements(decimal purchaseRequirements)
    {
        _purchaseRequirements = purchaseRequirements;
        return this;
    }

    public PlanLoyaltyProgramCommandBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }
}
