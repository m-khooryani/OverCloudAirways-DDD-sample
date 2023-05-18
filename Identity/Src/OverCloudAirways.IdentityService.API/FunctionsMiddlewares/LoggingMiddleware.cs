using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Diagnostics;
using Microsoft.Azure.Functions.Worker.Http;

namespace OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

internal class LoggingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        // Log the incoming request
        var correlationId = Guid.NewGuid();
        _logger.LogInformation("starting scope: {correlationId}", correlationId);
        using (_logger.BeginScope("{CorrelationId}", correlationId))
        {
            _logger.LogInformation($"Function '{context.FunctionId}' is starting execution at {DateTime.UtcNow}.");
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Invoke the next middleware or function
                await next(context);
                //throw new Exception();
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, $"Function '{context.FunctionId}' encountered a business rule error during execution.");

                // Set the response to a 409 status code
                var f = await context.GetHttpRequestDataAsync();
                var httpResponseData = f.CreateResponse(HttpStatusCode.Conflict);
                //var httpResponseData = context.BindingContext..GetBindings<HttpResponseData>().Values.Single();
                httpResponseData.StatusCode = HttpStatusCode.Conflict;
                await httpResponseData.WriteStringAsync("A business rule exception occurred.");


                var invocationResult = context.GetInvocationResult();
                invocationResult.Value = httpResponseData;
            }
            //catch (Exception ex)
            //{
            //    // Log any other exception
            //    _logger.LogError(ex, $"Function '{context.FunctionId}' encountered an error during execution.");
            //    throw;
            //}
            finally
            {
                // Log the response
                stopwatch.Stop();
                _logger.LogInformation($"Function '{context.FunctionId}' finished execution at {DateTime.UtcNow} with duration {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
