using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
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
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string validationToken = req.Query["validationToken"];
        _logger.LogInformation("validationToken");
        _logger.LogInformation(validationToken);

        return new OkResult();
    }
}
