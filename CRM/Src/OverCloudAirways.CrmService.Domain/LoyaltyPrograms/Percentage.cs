using OverCloudAirways.BuildingBlocks.Domain.Models;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms;

public class Percentage : ValueObject
{
    public decimal Value { get; }

    private Percentage()
    {
    }

    private Percentage(decimal value)
    {
        Value = value;
    }

    public static async Task<Percentage> OfAsync(decimal value)
    {
        await CheckRuleAsync(new PercentageAmountMustBeBetweenZeroAndOneHundredRule(value));
        return new Percentage(value);
    }
}
