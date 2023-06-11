using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.CrmService.API.FunctionsMiddlewares;

internal class JsonResponseMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IJsonSerializer _jsonSerializer;

    public JsonResponseMiddleware(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        await next(context);

        var invocationResult = context.GetInvocationResult();
        var httpRequest = await context.GetHttpRequestDataAsync();
        var httpResponseData = httpRequest.CreateResponse(HttpStatusCode.OK);
        if (invocationResult.Value is not null)
        {
            await httpResponseData.WriteStringAsync(_jsonSerializer.SerializeIndented(invocationResult.Value));
            invocationResult.Value = httpResponseData;
        }
    }
}