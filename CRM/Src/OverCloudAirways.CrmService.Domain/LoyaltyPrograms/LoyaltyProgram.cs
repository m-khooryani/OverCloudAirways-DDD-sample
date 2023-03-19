using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public class LoyaltyProgram : AggregateRoot<LoyaltyProgramId>
{
    public string Name { get; private set; }
    public decimal PurchaseRequirements { get; private set; }
    public Percentage DiscountPercentage { get; private set; }

    private LoyaltyProgram()
    {
    }

    public static async Task<LoyaltyProgram> PlanAsync(
        ILoyaltyProgramNameUniqueChecker uniqueChecker,
        LoyaltyProgramId id,
        string name,
        decimal purchaseRequirements,
        Percentage discountPercentage)
    {
        await CheckRuleAsync(new LoyaltyProgramNameShouldBeUniqueRule(name, uniqueChecker));

        var @event = new LoyaltyProgramPlannedDomainEvent(
            id,
            name,
            purchaseRequirements,
            discountPercentage);

        var loyaltyProgram = new LoyaltyProgram();
        loyaltyProgram.Apply(@event);

        return loyaltyProgram;
    }

    protected void When(LoyaltyProgramPlannedDomainEvent @event)
    {
        Id = @event.LoyaltyProgramId;
        Name = @event.Name;
        PurchaseRequirements = @event.PurchaseRequirements;
        DiscountPercentage = @event.DiscountPercentage;
    }
}
