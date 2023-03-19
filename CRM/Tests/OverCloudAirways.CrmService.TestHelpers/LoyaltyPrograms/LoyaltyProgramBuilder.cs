using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

namespace OverCloudAirways.CrmService.TestHelpers.LoyaltyPrograms;

public class LoyaltyProgramBuilder
{
    private ILoyaltyProgramNameUniqueChecker _uniqueChecker;
    private LoyaltyProgramId _loyaltyProgramId = LoyaltyProgramId.New();
    private string _name = "Gold Rewards";
    private decimal _purchaseRequirements = 10_000M;
    private Percentage _discountPercentage = Percentage.OfAsync(20M).Result;

    public async Task<LoyaltyProgram> BuildAsync()
    {
        return await LoyaltyProgram.PlanAsync(_uniqueChecker, _loyaltyProgramId, _name, _purchaseRequirements, _discountPercentage);
    }

    public LoyaltyProgramBuilder SetLoyaltyProgramNameUniqueChecker(ILoyaltyProgramNameUniqueChecker uniqueChecker)
    {
        _uniqueChecker = uniqueChecker;
        return this;
    }

    public LoyaltyProgramBuilder SetLoyaltyProgramId(LoyaltyProgramId loyaltyProgramId)
    {
        _loyaltyProgramId = loyaltyProgramId;
        return this;
    }

    public LoyaltyProgramBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public LoyaltyProgramBuilder SetPurchaseRequirements(decimal purchaseRequirements)
    {
        _purchaseRequirements = purchaseRequirements;
        return this;
    }

    public LoyaltyProgramBuilder SetDiscountPercentage(Percentage discountPercentage)
    {
        _discountPercentage = discountPercentage;
        return this;
    }
}
