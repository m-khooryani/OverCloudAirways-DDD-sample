using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;
using OverCloudAirways.IdentityService.Application.Users.Commands.Register;
using OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.API;

public class Function1
{
    private readonly CqrsInvoker _cqrsInvoker;
    private readonly IJsonSerializer _jsonSerializer;

    public Function1(
        CqrsInvoker cqrsInvoker, 
        IJsonSerializer jsonSerializer)
    {
        _cqrsInvoker = cqrsInvoker;
        _jsonSerializer = jsonSerializer;
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
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var request = _jsonSerializer.Deserialize<RegisterUserRequest>(requestBody);

        var registerUserCommand = new RegisterUserCommand(
            UserId.New(),
            request.UserType,
            request.Email,
            request.GivenName,
            request.Surname,
            request.Address);
        await _cqrsInvoker.CommandAsync(registerUserCommand);
    }
}

public class RegisterUserRequest
{
    public UserType UserType { get; set; }
    public string Email { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
}
