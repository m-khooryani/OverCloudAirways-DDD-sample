using DArch.Infrastructure;
using DArch.Samples.AppointmentService.IntegrationTests._SeedWork;
using OverCloudAirways.IdentityService.Application.Users.Commands.Register;
using OverCloudAirways.IdentityService.Domain.Users;
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
        var username = "admin";

        var registerUserCommand = new RegisterUserCommand(userId, username);
        await _invoker.CommandAsync(registerUserCommand);

        // 
        await _testFixture.ProcessLastOutboxMessageAsync();


    }
}
