using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.Customers;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public class LoyaltyProgram : AggregateRoot<LoyaltyProgramId>
{
    public string Name { get; private set; }
    public decimal PurchaseRequirements { get; private set; }
    public Percentage DiscountPercentage { get; private set; }
    public bool IsSuspended { get; private set; }

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

    public async Task EvaluateAsync(Customer customer)
    {
        await CheckRuleAsync(new OnlyActiveLoyaltyProgramCanBeEvaluatedRule(this));

        if (customer.LoyaltyPoints >= PurchaseRequirements)
        {
            var qualifiedEvent = new LoyaltyProgramQualifiedForCustomerDomainEvent(Id, customer.Id);
            Apply(qualifiedEvent);
        }
        var evaluatedEvent = new LoyaltyProgramEvaluatedForCustomerDomainEvent(Id, customer.Id);
        Apply(evaluatedEvent);
    }

    public void Suspend()
    {
        var @event = new LoyaltyProgramSuspendedDomainEvent(Id);
        Apply(@event);
    }

    public void Reactivate()
    {
        var @event = new LoyaltyProgramReactivatedDomainEvent(Id);
        Apply(@event);
    }

    protected void When(LoyaltyProgramPlannedDomainEvent @event)
    {
        Id = @event.LoyaltyProgramId;
        Name = @event.Name;
        PurchaseRequirements = @event.PurchaseRequirements;
        DiscountPercentage = @event.DiscountPercentage;
        IsSuspended = false;
    }

    protected void When(LoyaltyProgramQualifiedForCustomerDomainEvent _)
    {
    }

    protected void When(LoyaltyProgramEvaluatedForCustomerDomainEvent _)
    {
    }

    protected void When(LoyaltyProgramSuspendedDomainEvent _)
    {
        IsSuspended = true;
    }

    protected void When(LoyaltyProgramReactivatedDomainEvent _)
    {
        IsSuspended = false;
    }
}
