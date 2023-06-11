using System.Diagnostics;
using System.Net;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Domain.Exceptions;

namespace OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

internal class LoggingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        _logger.LogInformation($"Function '{context.FunctionId}' is starting execution at {DateTime.UtcNow}.");
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await next(context);
        }
        catch (BusinessRuleValidationException ex)
        {
            await HandleException(context, ex, "A business rule exception occurred.", HttpStatusCode.Conflict);
        }
        catch (EntityAlreadyExistsException ex)
        {
            await HandleException(context, ex, "An entity with the same ID already exists.", HttpStatusCode.Conflict);
        }
        catch (EntityNotFoundException ex)
        {
            await HandleException(context, ex, "The specified entity was not found.", HttpStatusCode.NotFound);
        }
        catch (ValidationException ex)
        {
            await HandleException(context, ex, "A validation exception occurred: " + ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex, "An unexpected error occurred.", HttpStatusCode.InternalServerError);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Function '{context.FunctionId}' finished execution at {DateTime.UtcNow} with duration {stopwatch.ElapsedMilliseconds} ms.");
        }
    }

    private async Task HandleException(FunctionContext context, Exception ex, string message, HttpStatusCode statusCode)
    {
        _logger.LogError(ex, $"Function '{context.FunctionId}' encountered an error during execution.");

        var httpRequest = await context.GetHttpRequestDataAsync();
        var httpResponseData = httpRequest.CreateResponse(statusCode);
        await httpResponseData.WriteStringAsync(message);

        var invocationResult = context.GetInvocationResult();
        invocationResult.Value = httpResponseData;
    }
}
