using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.CrmService.Application.LoyaltyPrograms.Queries.GetInfo;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.IntegrationTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.LoyaltyPrograms;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.CrmService.IntegrationTests.LoyaltyPrograms;

[Collection("CRM")]
public class LoyaltyProgramTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public LoyaltyProgramTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task PlanLoyaltyProgram_LoyaltyProgramShouldBePlaned_AndAllPropertiesShouldMatch()
    {
        await _testFixture.ResetAsync();

        var loyaltyProgramId = LoyaltyProgramId.New();

        // Plan LoyaltyProgram 
        var planLoyaltyProgramCommand = new PlanLoyaltyProgramCommandBuilder()
            .SetLoyaltyProgramId(loyaltyProgramId)
            .Build();
        await _invoker.CommandAsync(planLoyaltyProgramCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // LoyaltyProgram Query
        var query = new GetLoyaltyProgramInfoQuery(loyaltyProgramId.Value);
        var loyaltyProgram = await _invoker.QueryAsync(query);

        // Assert
        Assert.NotNull(loyaltyProgram);
        Assert.Equal(loyaltyProgramId.Value, loyaltyProgram.Id);
        Assert.Equal(planLoyaltyProgramCommand.Name, loyaltyProgram.Name);
        Assert.Equal(planLoyaltyProgramCommand.PurchaseRequirements, loyaltyProgram.PurchaseRequirements);
        Assert.Equal(planLoyaltyProgramCommand.DiscountPercentage, loyaltyProgram.DiscountPercentage);
    }
}
