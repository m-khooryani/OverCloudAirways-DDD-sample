using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Application.Queries;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.QueryPipelines;

internal class QueryLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{
    private readonly ILogger _logger;

    public QueryLoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{request.GetType().Name} is processing: {Environment.NewLine}{JsonConvert.SerializeObject(request, Formatting.Indented)}");
        try
        {
            TResponse result = await next();
            if (typeof(TResponse) != typeof(Unit))
            {
                _logger.LogInformation($"Result: {Environment.NewLine}{JsonConvert.SerializeObject(result, Formatting.Indented)}");
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled Exception:{Environment.NewLine}{ex}");
            throw;
        }
        finally
        {
            _logger.LogDebug($"{request.GetType().Name} is processed.");
        }
    }
}
