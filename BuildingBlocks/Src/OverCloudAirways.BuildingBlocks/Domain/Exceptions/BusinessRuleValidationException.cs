using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Domain.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BrokenRule { get; }
    public string Details { get; }

    internal BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.TranslationKey)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.TranslationKey;
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.TranslationKey}";
    }
}
