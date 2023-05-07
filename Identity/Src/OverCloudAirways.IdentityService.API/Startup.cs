using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net;
using System.Diagnostics;
using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(OverCloudAirways.IdentityService.API.Startup))]
namespace OverCloudAirways.IdentityService.API;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
    }
}

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

internal class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthorizationMiddleware> _logger;

    public AuthorizationMiddleware(
        IConfiguration configuration,
        ILogger<AuthorizationMiddleware> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (!context.BindingContext.BindingData.ContainsKey("Headers"))
        {
            await SetUnauthorizedResponse(context, "Authorization header not found.");
            return;
        }
        var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)context.BindingContext.BindingData["Headers"]);

        if (!headers.ContainsKey("Authorization"))
        {
            await SetUnauthorizedResponse(context, "Authorization header not found.");
            return;
        }
        var authorization = AuthenticationHeaderValue.Parse(headers["Authorization"]);
        var bearerToken = authorization.Parameter;

        var configManager =
            new ConfigurationManager<OpenIdConnectConfiguration>(
                _configuration["Authority"],
                new OpenIdConnectConfigurationRetriever());

        var config = await configManager.GetConfigurationAsync();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            //ValidIssuer = "to be added",
            ValidateAudience = false,
            //ValidAudience = "to be added",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = config.SigningKeys
        };

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(bearerToken, validationParameters, out SecurityToken validatedToken);
        }
        catch (Exception e)
        {
            await SetUnauthorizedResponse(context, e.Message);
            return;
        }

        await next(context);
    }

    private async Task SetUnauthorizedResponse(FunctionContext context, string message)
    {
        _logger.LogWarning($"Authorization failed: {message}");
        var httpRequestData = await context.GetHttpRequestDataAsync();
        var response = httpRequestData.CreateResponse();

        response.StatusCode = HttpStatusCode.Unauthorized;
        await response.WriteStringAsync(message);

        context.GetInvocationResult().Value = response;
    }
}