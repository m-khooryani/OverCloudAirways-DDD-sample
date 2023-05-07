using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

namespace OverCloudAirways.IdentityService.API;

public class Function1
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public Function1(
        IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _logger = loggerFactory.CreateLogger<Function1>();
    }

    [Function("Function1Identity")]
    [Authorized("Admin")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        throw new NotImplementedException();
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions! (Identity)");
        response.WriteString($"Config: {_configuration["FUNCTIONS_EXTENSION_VERSION"]}");

        return response;
    }
}
