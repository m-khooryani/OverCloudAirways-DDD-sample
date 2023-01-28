using System.Diagnostics.CodeAnalysis;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;

[ExcludeFromCodeCoverage]
public abstract class Test
{
    public static async Task AssertViolatedRuleAsync<TRule>(Func<Task> testDelegate)
        where TRule : class, IBusinessRule
    {
        var businessRuleValidationException = await Assert.ThrowsAsync<BusinessRuleValidationException>(testDelegate);
        if (businessRuleValidationException != null)
        {
            Assert.IsType<TRule>(businessRuleValidationException.BrokenRule);
        }
    }
}
