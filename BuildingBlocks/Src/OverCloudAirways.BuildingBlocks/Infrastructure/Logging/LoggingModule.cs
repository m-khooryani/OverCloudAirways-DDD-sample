using Autofac;
using Microsoft.Extensions.Logging;

namespace DArch.Infrastructure.Logging;

public class LoggingModule : Module
{
    private readonly ILogger _logger;

    public LoggingModule(ILoggerFactory loggerFactory, string categoryName)
    {
        _logger = loggerFactory.CreateLogger(categoryName);
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_logger)
            .As<ILogger>()
            .SingleInstance();
    }
}
