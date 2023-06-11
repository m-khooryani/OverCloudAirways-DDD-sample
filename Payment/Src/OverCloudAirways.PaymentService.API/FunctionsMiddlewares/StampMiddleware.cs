using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace OverCloudAirways.PaymentService.API.FunctionsMiddlewares;

internal class StampMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public StampMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var correlationId = Guid.NewGuid();
        _logger.LogInformation("starting scope: {correlationId}", correlationId);
        using (_logger.BeginScope("{CorrelationId}", correlationId))
        {
            await next(context);
        }
    }
}