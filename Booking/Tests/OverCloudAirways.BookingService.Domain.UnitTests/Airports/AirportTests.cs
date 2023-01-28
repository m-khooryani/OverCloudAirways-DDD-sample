using NSubstitute;
using OverCloudAirways.BookingService.Domain.Airports;
using OverCloudAirways.BookingService.Domain.Airports.Rules;
using OverCloudAirways.BookingService.Domain.UnitTests._SeedWork;
using OverCloudAirways.BookingService.TestHelpers.Airports;
using Xunit;

namespace OverCloudAirways.BookingService.Domain.UnitTests.Airports;

public class AirportTests : Test
{
    [Fact]
    public async Task CreateAirport_Given_Duplicate_Code_Should_Throw_Business_Error()
    {
        // Arrange
        var uniqueCodeChecker = Substitute.For<IAirportCodeUniqueChecker>();
        uniqueCodeChecker.IsUniqueAsync(Arg.Any<string>()).Returns(false);
        var builder = new AirportBuilder()
            .SetAirportCodeUniqueChecker(uniqueCodeChecker);

        // Act, Assert
        await AssertViolatedRuleAsync<AirportCodeShouldBeUniqueRule>(async () =>
        {
            _ = await builder.BuildAsync();
        });
    }
}
