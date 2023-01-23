using Microsoft.Extensions.Logging;

namespace DArch.Samples.AppointmentService.IntegrationTests._SeedWork;

internal class IntegratinTestLogger : ILogger
{
    private readonly Action<string> _logAction;
    private readonly LogLevel _logLevel;

    public IntegratinTestLogger(
        Action<string> logAction, 
        LogLevel logLevel)
    {
        _logAction = logAction;
        _logLevel = logLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _logLevel;
    }

    public void Log<TState>(
        LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        _logAction($"[{logLevel}] {state}");
    }
}
