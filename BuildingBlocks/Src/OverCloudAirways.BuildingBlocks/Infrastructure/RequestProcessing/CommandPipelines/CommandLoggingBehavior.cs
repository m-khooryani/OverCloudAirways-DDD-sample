using DArch.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DArch.Infrastructure.Processing;

internal class CommandLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICommand<TResponse>
{
    private readonly ILogger _logger;

    public CommandLoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{requestName} is processing: {environment}{request}",
            request.GetType().Name,
            Environment.NewLine,
            JsonConvert.SerializeObject(request, Formatting.Indented)
        );
        try
        {
            TResponse result = await next();
            if (typeof(TResponse) != typeof(Unit))
            {
                _logger.LogInformation("Result: {environment}{result}",
                    Environment.NewLine,
                    result);
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            throw;
        }
        finally
        {
            _logger.LogInformation($"request {request.GetType().Name} is processed.");
        }
    }
}
