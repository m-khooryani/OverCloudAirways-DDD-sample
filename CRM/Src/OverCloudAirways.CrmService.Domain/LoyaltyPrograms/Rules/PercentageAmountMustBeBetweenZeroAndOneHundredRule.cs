using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

internal class PercentageAmountMustBeBetweenZeroAndOneHundredRule : IBusinessRule
{
    private readonly decimal _value;

    public PercentageAmountMustBeBetweenZeroAndOneHundredRule(decimal value)
    {
        _value = value;
    }

    public string TranslationKey => "Percentage_Amount_Must_Be_Between_Zero_And_OneHundred";

    public Task<bool> IsFollowedAsync()
    {
        return Task.FromResult(0M <= _value && _value < 100M);
    }
}
