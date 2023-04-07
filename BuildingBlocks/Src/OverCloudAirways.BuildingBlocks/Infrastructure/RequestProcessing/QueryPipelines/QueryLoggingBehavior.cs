using MediatR;
using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Application.Queries;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.RequestProcessing.QueryPipelines;

internal class QueryLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{
    private readonly ILogger _logger;
    private IJsonSerializer _jsonSerializer;

    public QueryLoggingBehavior(
        ILogger logger,
        IJsonSerializer jsonSerializer)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{request.GetType().Name} is processing: {Environment.NewLine}{_jsonSerializer.SerializeIndented(request)}");
        try
        {
            TResponse result = await next();
            if (typeof(TResponse) != typeof(Unit))
            {
                _logger.LogInformation($"Result: {Environment.NewLine}{_jsonSerializer.SerializeIndented(result)}");
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
