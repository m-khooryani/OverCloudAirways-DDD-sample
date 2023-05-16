using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace OverCloudAirways.IdentityService.API.GraphIntegration;

public class NotificationFunction
{
    private readonly ILogger _logger;

    public NotificationFunction(
        ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<NotificationFunction>();
    }

    [Function("notifications")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        return new OkResult();
    }
}
