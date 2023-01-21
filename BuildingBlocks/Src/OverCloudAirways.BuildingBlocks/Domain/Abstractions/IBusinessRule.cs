namespace OverCloudAirways.BuildingBlocks.Domain.Abstractions;

public interface IBusinessRule
{
    string TranslationKey { get; }
    Task<bool> IsFollowedAsync();
}
