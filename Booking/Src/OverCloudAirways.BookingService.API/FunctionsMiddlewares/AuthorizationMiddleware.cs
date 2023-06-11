using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using System.Reflection;
using System.Collections.Concurrent;

namespace OverCloudAirways.BookingService.API.FunctionsMiddlewares;

internal class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<AuthorizationMiddleware> _logger;
    private readonly ConcurrentDictionary<string, AuthorizedAttribute> _authorizedAttributeCache = new();

    public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var functionEntryPoint = context.FunctionDefinition.EntryPoint;
        var functionAuthorizeAttribute = GetAuthorizedAttribute(functionEntryPoint);

        if (functionAuthorizeAttribute != null)
        {
            if (context.Items.TryGetValue("UserRole", out object userRole) && userRole is string role &&
                functionAuthorizeAttribute.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase))
            {
                await next(context);
                return;
            }

            await SetUnauthorizedResponse(context, "Forbidden Access.");
        }
        else
        {
            await next(context);
        }
    }

    private static MethodInfo GetMethodInfo(string functionEntryPoint)
    {
        var parts = functionEntryPoint.Split('.');
        var typeName = string.Join('.', parts, 0, parts.Length - 1);
        var methodName = parts[^1];
        var assemblyType = Type.GetType(typeName);
        return assemblyType.GetMethod(methodName);
    }

    private AuthorizedAttribute GetAuthorizedAttribute(string functionEntryPoint)
    {
        return _authorizedAttributeCache.GetOrAdd(functionEntryPoint, entryPoint =>
        {
            var methodInfo = GetMethodInfo(entryPoint);
            return methodInfo.GetCustomAttribute(typeof(AuthorizedAttribute), false) as AuthorizedAttribute;
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
