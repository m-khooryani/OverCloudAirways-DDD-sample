using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OverCloudAirways.BuildingBlocks.Infrastructure;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;
using OverCloudAirways.IdentityService.Application.Users.Queries.GetInfo;

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
    public async Task<HttpResponseData> GetUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [FromQuery] Guid userId)
    {
        var user = await _cqrsInvoker.QueryAsync(new GetUserInfoQuery(userId));

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(user.GivenName);

        return response;
    }
}
