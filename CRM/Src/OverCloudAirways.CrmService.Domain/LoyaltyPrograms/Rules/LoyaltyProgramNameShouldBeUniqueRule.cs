using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;

internal class LoyaltyProgramNameShouldBeUniqueRule : IBusinessRule
{
    private readonly string _name;
    private readonly ILoyaltyProgramNameUniqueChecker _uniqueChecker;

    public LoyaltyProgramNameShouldBeUniqueRule(
        string name, 
        ILoyaltyProgramNameUniqueChecker uniqueChecker)
    {
        _name = name;
        _uniqueChecker = uniqueChecker;
    }

    public string TranslationKey => "LoyaltyProgram_Name_Should_Be_Unique";

    public async Task<bool> IsFollowedAsync()
    {
        return await _uniqueChecker.IsUniqueAsync(_name);
    }
}