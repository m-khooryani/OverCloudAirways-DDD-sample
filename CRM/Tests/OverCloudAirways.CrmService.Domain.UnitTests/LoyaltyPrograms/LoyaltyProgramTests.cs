using NSubstitute;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Events;
using OverCloudAirways.CrmService.Domain.LoyaltyPrograms.Rules;
using OverCloudAirways.CrmService.Domain.UnitTests._SeedWork;
using OverCloudAirways.CrmService.TestHelpers.Customers;
using OverCloudAirways.CrmService.TestHelpers.LoyaltyPrograms;
using Xunit;

namespace OverCloudAirways.CrmService.Domain.UnitTests.LoyaltyPrograms;

public class LoyaltyProgramTests : Test
{
    [Fact]
    public async Task PlanLoyaltyProgram_Given_Valid_Input_Should_Successfully_Plan_LoyaltyProgram_And_Publish_Event()
    {
        // Arrange
        var loyaltyProgramId = LoyaltyProgramId.New();
        var name = "Gold Rewards";
        var purchaseRequirements = 10_000M;
        var percentageDiscount = await Percentage.OfAsync(20M);
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(name).Returns(true);

        // Act
        var loyaltyProgram = await new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker)
            .SetLoyaltyProgramId(loyaltyProgramId)
            .SetPurchaseRequirements(purchaseRequirements)
            .SetDiscountPercentage(percentageDiscount)
            .BuildAsync();

        // Assert
        Assert.Equal(loyaltyProgramId, loyaltyProgram.Id);
        Assert.Equal(name, loyaltyProgram.Name);
        Assert.Equal(purchaseRequirements, loyaltyProgram.PurchaseRequirements);
        Assert.Equal(percentageDiscount, loyaltyProgram.DiscountPercentage);
        Assert.False(loyaltyProgram.IsSuspended);
        AssertPublishedDomainEvent<LoyaltyProgramPlannedDomainEvent>(loyaltyProgram);
    }

    [Fact]
    public async Task PlanLoyaltyProgram_Given_Duplicate_Name_Should_Throw_Business_Error()
    {
        // Arrange
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(Arg.Any<string>()).Returns(false);
        var builder = new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker);

        // Act, Assert
        await AssertViolatedRuleAsync<LoyaltyProgramNameShouldBeUniqueRule>(async () =>
        {
            _ = await builder.BuildAsync();
        });
    }

    [Fact]
    public async Task EvaluateLoyaltyProgram_Given_NotQualifiedCustomer_Input_Should_Not_Qualify_And_Publish_Event()
    {
        // Arrange
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);
        var loyaltyProgram = await new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker)
            .BuildAsync();
        var customer = new CustomerBuilder().Build();

        // Act
        loyaltyProgram.Evaluate(customer);

        // Assert
        AssertPublishedDomainEvent<LoyaltyProgramEvaluatedForCustomerDomainEvent>(loyaltyProgram);
        AssertNotPublishedDomainEvent<LoyaltyProgramQualifiedForCustomerDomainEvent>(loyaltyProgram);
    }

    [Fact]
    public async Task EvaluateLoyaltyProgram_Given_Valid_Input_Should_Successfully_Qualify_LoyaltyProgram_And_Publish_Event()
    {
        // Arrange
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);
        var loyaltyProgram = await new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker)
            .BuildAsync();
        var customer = new CustomerBuilder().Build();

        // Act
        customer.CollectLoyaltyPoints(1_000_000M);
        loyaltyProgram.Evaluate(customer);

        // Assert
        AssertPublishedDomainEvent<LoyaltyProgramEvaluatedForCustomerDomainEvent>(loyaltyProgram);
        AssertPublishedDomainEvent<LoyaltyProgramQualifiedForCustomerDomainEvent>(loyaltyProgram);
    }

    [Fact]
    public async Task SuspendLoyaltyProgram_Given_Valid_Input_Should_Successfully_Suspend_LoyaltyProgram_And_Publish_Event()
    {
        // Arrange
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);
        var loyaltyProgram = await new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker)
            .BuildAsync();

        // Act
        loyaltyProgram.Suspend();

        // Assert
        Assert.True(loyaltyProgram.IsSuspended);
        AssertPublishedDomainEvent<LoyaltyProgramSuspendedDomainEvent>(loyaltyProgram);
    }

    [Fact]
    public async Task ReactivateLoyaltyProgram_Given_Valid_Input_Should_Successfully_Reactivate_LoyaltyProgram_And_Publish_Event()
    {
        // Arrange
        var uniqueNameChecker = Substitute.For<ILoyaltyProgramNameUniqueChecker>();
        uniqueNameChecker.IsUniqueAsync(Arg.Any<string>()).Returns(true);
        var loyaltyProgram = await new LoyaltyProgramBuilder()
            .SetLoyaltyProgramNameUniqueChecker(uniqueNameChecker)
            .BuildAsync();

        // Act
        loyaltyProgram.Suspend();
        loyaltyProgram.Reactivate();

        // Assert
        Assert.False(loyaltyProgram.IsSuspended);
        AssertPublishedDomainEvent<LoyaltyProgramReactivatedDomainEvent>(loyaltyProgram);
    }
}
