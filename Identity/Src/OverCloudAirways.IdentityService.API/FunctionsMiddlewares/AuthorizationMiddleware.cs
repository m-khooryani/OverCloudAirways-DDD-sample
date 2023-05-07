using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using System.Reflection;

namespace OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

internal class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<AuthorizationMiddleware> _logger;

    public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            string functionEntryPoint = context.FunctionDefinition.EntryPoint;
            Type assemblyType = Type.GetType(functionEntryPoint.Substring(0, functionEntryPoint.LastIndexOf('.')));
            MethodInfo methodInfo = assemblyType.GetMethod(functionEntryPoint.Substring(functionEntryPoint.LastIndexOf('.') + 1));
            if (methodInfo.GetCustomAttribute(typeof(AuthorizedAttribute), false) is AuthorizedAttribute functionAuthorizeAttribute)
            {
                if (context.Items.ContainsKey("UserRole") && context.Items["UserRole"] != null)
                {
                    var role = context.Items["UserRole"].ToString();
                    if (!functionAuthorizeAttribute.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase))
                    {
                        await SetUnauthorizedResponse(context, "Forbidden Access.");
                        return;
                    }
                }
                else
                {
                    await SetUnauthorizedResponse(context, "Forbidden Access.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Unhandled exception: {exception}", ex.Message);
            await SetUnauthorizedResponse(context, string.Empty);
            return;
        }
        await next(context);
    }

    private async Task SetUnauthorizedResponse(FunctionContext context, string message)
    {
        _logger.LogWarning($"Authorization failed: {message}");
        var httpRequestData = await context.GetHttpRequestDataAsync();
        var response = httpRequestData.CreateResponse();

        response.StatusCode = HttpStatusCode.Forbidden;
        await response.WriteStringAsync(message);

        context.GetInvocationResult().Value = response;
    }
}