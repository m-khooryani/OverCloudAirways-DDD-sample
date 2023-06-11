using System.Diagnostics;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace OverCloudAirways.CrmService.API.FunctionsMiddlewares;

internal class LoggingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        _logger.LogInformation($"Function '{context.FunctionId}' is starting execution at {DateTime.UtcNow}.");
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Function '{context.FunctionId}' encountered a business rule error during execution.");

            // Set the response to a 409 status code
            var httpRequest = await context.GetHttpRequestDataAsync();
            var httpResponseData = httpRequest.CreateResponse(HttpStatusCode.Conflict);
            await httpResponseData.WriteStringAsync("A business rule exception occurred.");

            var invocationResult = context.GetInvocationResult();
            invocationResult.Value = httpResponseData;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Function '{context.FunctionId}' finished execution at {DateTime.UtcNow} with duration {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
