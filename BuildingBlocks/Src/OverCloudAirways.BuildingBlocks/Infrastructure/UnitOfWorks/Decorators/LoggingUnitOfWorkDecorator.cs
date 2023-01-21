using Microsoft.Extensions.Logging;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.BuildingBlocks.Infrastructure.UnitOfWorks.Decorators;

internal class LoggingUnitOfWorkDecorator : IUnitOfWork
{
    private readonly IUnitOfWork _decorated;
    private readonly ILogger _logger;
    private bool _commited;

    public LoggingUnitOfWorkDecorator(
        IUnitOfWork decorated,
        ILogger logger)
    {
        _decorated = decorated;
        _logger = logger;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Commiting changes...");
        if (_commited)
        {
            throw new Exception("UoW can not be commited twice within a scope.");
        }

        var changes = await _decorated.CommitAsync(cancellationToken);
        _logger.LogInformation($"{changes} changes just commited");

        _commited = true;
        return changes;
    }
}
