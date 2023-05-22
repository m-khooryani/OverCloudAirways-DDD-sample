using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.IdentityService.Application.Users.Commands.Register;
using OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;
using OverCloudAirways.IdentityService.Domain.Users;
using OverCloudAirways.IdentityService.IntegrationTests._SeedWork;
using Xunit;
using Xunit.Abstractions;

namespace OverCloudAirways.IdentityService.IntegrationTests.Users;

[Collection("Database collection")]
public class UserTests
{
    private readonly CqrsInvoker _invoker = TestFixture.Invoker;
    private readonly TestFixture _testFixture;

    public UserTests(TestFixture fixture, ITestOutputHelper output)
    {
        _testFixture = fixture;
        TestFixture.Output = output;
    }

    [Fact]
    public async Task Test1()
    {
        await _testFixture.ResetAsync();

        var userId = UserId.New();
        var givenName = "admin given name";
        var surname = "admin surname";
        var email = "test@domain.com";

        var registerUserCommand = new RegisterCustomerUserCommand(userId, email, givenName, surname);
        await _invoker.CommandAsync(registerUserCommand);

        // Process Registered Policy
        await _testFixture.ProcessLastOutboxMessageAsync();
        // Process Project Read-Model
        await _testFixture.ProcessLastOutboxMessageAsync();

        // ReadUser Query
        var query = new GetUserInfoQuery(userId.Value);
        var user = await _invoker.QueryAsync(query);

        Assert.NotNull(user);
        Assert.Equal(userId.Value, user.Id);
        Assert.Equal(UserType.Customer, user.UserType);
        Assert.Equal(email, user.Email);
        Assert.Equal(givenName, user.GivenName);
        Assert.Equal(surname, user.Surname);
    }
}
