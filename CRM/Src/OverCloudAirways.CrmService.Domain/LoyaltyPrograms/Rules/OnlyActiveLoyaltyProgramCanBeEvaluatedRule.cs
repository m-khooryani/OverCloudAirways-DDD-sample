using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

internal class OnlyActiveLoyaltyProgramCanBeEvaluatedRule : IBusinessRule
{
    private readonly LoyaltyProgram _loyaltyProgram;

    public OnlyActiveLoyaltyProgramCanBeEvaluatedRule(LoyaltyProgram loyaltyProgram)
    {
        _loyaltyProgram = loyaltyProgram;
    }

    public string TranslationKey => "Only_Active_LoyaltyProgram_Can_Be_Evaluated";

    public async Task<bool> IsFollowedAsync()
    {
        return await Task.FromResult(!_loyaltyProgram.IsSuspended);
    }
}