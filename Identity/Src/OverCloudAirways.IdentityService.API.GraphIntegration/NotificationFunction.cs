using Microsoft.AspNetCore.Http;
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, 
        [FromQuery] string validationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        _logger.LogInformation("validationToken");
        _logger.LogInformation(validationToken);

        return new OkResult();
    }
}
