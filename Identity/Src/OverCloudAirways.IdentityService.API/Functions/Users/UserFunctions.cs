using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;
using OverCloudAirways.IdentityService.Application.Users.Commands.Register;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.API.Functions.Users;

public class UserFunctions
{
    private readonly CqrsInvoker _cqrsInvoker;
    private readonly IJsonSerializer _jsonSerializer;

    public UserFunctions(
        CqrsInvoker cqrsInvoker,
        IJsonSerializer jsonSerializer)
    {
        _cqrsInvoker = cqrsInvoker;
        _jsonSerializer = jsonSerializer;
    }

    [Function("register-user")]
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
