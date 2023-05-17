using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        var appConfig = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var instrumentationKey = appConfig.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.ApplicationInsights(new TelemetryConfiguration(instrumentationKey), TelemetryConverter.Traces);

        //var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
        services.AddLogging(o => o.AddSerilog(logConfig.CreateLogger(), dispose: true));
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
