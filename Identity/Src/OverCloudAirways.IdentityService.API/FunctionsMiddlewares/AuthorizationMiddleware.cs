using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using System.Reflection;
using System.Collections.Concurrent;

namespace OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

internal class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<AuthorizationMiddleware> _logger;
    private readonly ConcurrentDictionary<string, MethodInfo> _methodInfoCache = new();

    public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            string functionEntryPoint = context.FunctionDefinition.EntryPoint;
            MethodInfo methodInfo = GetMethodInfo(functionEntryPoint);

            if (methodInfo.GetCustomAttribute(typeof(AuthorizedAttribute), false) is AuthorizedAttribute functionAuthorizeAttribute)
            {
                if (context.Items.TryGetValue("UserRole", out object userRole) && userRole is string role)
                {
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

    private MethodInfo GetMethodInfo(string functionEntryPoint)
    {
        return _methodInfoCache.GetOrAdd(functionEntryPoint, entryPoint =>
        {
            var parts = entryPoint.Split('.');
            var typeName = string.Join('.', parts, 0, parts.Length - 1);
            var methodName = parts[^1];
            var assemblyType = Type.GetType(typeName);
            return assemblyType.GetMethod(methodName);
        });
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
