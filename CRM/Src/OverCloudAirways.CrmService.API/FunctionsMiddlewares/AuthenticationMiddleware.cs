using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Extensions.Configuration;

namespace OverCloudAirways.CrmService.API.FunctionsMiddlewares;

internal class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationMiddleware> _logger;

    public AuthenticationMiddleware(
        IConfiguration configuration,
        ILogger<AuthenticationMiddleware> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            if (!context.BindingContext.BindingData.ContainsKey("Headers"))
            {
                await SetUnauthenticatedResponse(context, "Authorization header not found.");
                return;
            }
            var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>((string)context.BindingContext.BindingData["Headers"]);

            if (!headers.ContainsKey("Authorization"))
            {
                await SetUnauthenticatedResponse(context, "Authorization header not found.");
                return;
            }
            var authorization = AuthenticationHeaderValue.Parse(headers["Authorization"]);
            var bearerToken = authorization.Parameter;

            var configManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(
                    _configuration["Authority"],
                    new OpenIdConnectConfigurationRetriever());

            var config = await configManager.GetConfigurationAsync();

            // Note: this is a simplified token validation!
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = config.SigningKeys
            };

            var claims = new JwtSecurityTokenHandler()
                .ValidateToken(bearerToken, validationParameters, out SecurityToken validatedToken);

            var role = claims.Claims
                .SingleOrDefault(x => x.Type.ToLower() == "extension_userrole");
            context.Items.Add("UserRole", role is null ? "Customer" : role.Value);
        }
        catch (Exception)
        {
            await SetUnauthenticatedResponse(context, "Authentication Failed");
            return;
        }

        await next(context);
    }

    private async Task SetUnauthenticatedResponse(FunctionContext context, string message)
    {
        _logger.LogWarning($"Authentication failed: {message}");
        var httpRequestData = await context.GetHttpRequestDataAsync();
        var response = httpRequestData.CreateResponse();

        response.StatusCode = HttpStatusCode.Unauthorized;
        await response.WriteStringAsync(message);

        context.GetInvocationResult().Value = response;
    }
}
