using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;
using OverCloudAirways.IdentityService.Application.Users.Commands.Register;
using OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.API;

public class Function1
{
    private readonly CqrsInvoker _cqrsInvoker;

    public Function1(CqrsInvoker cqrsInvoker)
    {
        _cqrsInvoker = cqrsInvoker;
    }

    [Function("users")]
    [Authorized("Admin")]
    public async Task<UserDto> GetUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [FromQuery] Guid userId)
    {
        var user = await _cqrsInvoker.QueryAsync(new GetUserInfoQuery(userId));

        return user;
    }

    [Function("create-user")]
    [Authorized("Admin")]
    public async Task CreateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req)
    {
        var registerUserCommand = new RegisterUserCommand(
            UserId.New(),
            UserType.Admin,
            "mojtaba.khooryani@polestar.com",
            "Mojtaba",
            "Khooryani",
            "Tolereds");
        await _cqrsInvoker.CommandAsync(registerUserCommand);
    }
}
