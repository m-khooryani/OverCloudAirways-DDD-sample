using Microsoft.Extensions.Logging;

namespace OverCloudAirways.CrmService.IntegrationTests._SeedWork;

internal class LogToActionLoggerProvider : ILoggerProvider
{
    private readonly Action<string> _logAction;
    private readonly IDictionary<string, LogLevel> _logLevels;

    public LogToActionLoggerProvider(
        IDictionary<string, LogLevel> logLevels,
        Action<string> logAction)
    {
        _logLevels = logLevels;
        _logAction = logAction;
    }

    public ILogger CreateLogger(string categoryName)
    {
        foreach (var logLevel in _logLevels)
        {
            if (categoryName.StartsWith(logLevel.Key.TrimEnd('*')))
            {
                return new IntegratinTestLogger(_logAction, logLevel.Value);
            }
        }
        return new IntegratinTestLogger(_logAction, _logLevels["Default"]);
    }

    public void Dispose()
    {
    }
}
