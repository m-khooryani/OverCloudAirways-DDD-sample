using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OverCloudAirways.IdentityService.API.GraphIntegration;

public class NotificationFunction
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public NotificationFunction(
        ILoggerFactory loggerFactory,
        IConfiguration configuration)
    {
        _logger = loggerFactory.CreateLogger<NotificationFunction>();
        _configuration = configuration;
    }

    [Function("notifications")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request."); 
        _logger.LogInformation("Retrieving projectId: {projectId}", 123); // just a sample log

        if (!IsAuthorized(req))
        {
            _logger.LogInformation("Provided credentials are invalid.");
            return new UnauthorizedResult();
        }

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        _logger.LogInformation($"Request Body: {requestBody}");

        return new OkResult();
    }

    private bool IsAuthorized(HttpRequestData req)
    {
        var username = _configuration["AuthUsername"];
        var password = _configuration["AuthPassword"];

        if (req.Headers.TryGetValues("Authorization", out var authHeaders))
        {
            var authHeader = authHeaders.FirstOrDefault()?["Basic ".Length..].Trim();
            if (authHeader is null)
            {
                return false;
            }
            byte[] decodedBytes = Convert.FromBase64String(authHeader);
            var decodedText = Encoding.UTF8.GetString(decodedBytes);

            string[] parts = decodedText.Split(':');
            string providedUsername = parts[0];
            string providedPassword = parts[1];

            if (username == providedUsername && password == providedPassword)
            {
                _logger.LogInformation("Provided credentials are valid.");
                return true;
            }
            else
            {
                _logger.LogInformation("Provided credentials are invalid.");
                return false;
            }
        }
        return false;
    }
}
