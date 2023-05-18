using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OverCloudAirways.IdentityService.API;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;
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
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.ApplicationInsights(new TelemetryConfiguration(instrumentationKey), TelemetryConverter.Traces);

        services.AddLogging(o => o.AddSerilog(logConfig.CreateLogger(), dispose: true));

        services.RegisterApplicationComponents(appConfig);
    })
    .ConfigureFunctionsWorkerDefaults(workerApplicationBuilder =>
    {
        workerApplicationBuilder.UseMiddleware<LoggingMiddleware>();
        workerApplicationBuilder.UseMiddleware<AuthenticationMiddleware>();
        workerApplicationBuilder.UseMiddleware<AuthorizationMiddleware>();
    })
    .Build();

host.Run();
