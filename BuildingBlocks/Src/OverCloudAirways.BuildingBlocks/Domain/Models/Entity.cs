using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;

namespace OverCloudAirways.BuildingBlocks.Domain.Models;

public abstract class Entity<TKey>
    where TKey : TypedId
{
    public TKey Id { get; protected set; }

    protected static async Task CheckRuleAsync(IBusinessRule rule)
    {
        if (!await rule.IsFollowedAsync())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
