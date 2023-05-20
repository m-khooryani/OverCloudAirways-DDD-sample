using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OverCloudAirways.IdentityService.API.GraphIntegration;
using Serilog;
using Serilog.Events;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables()
            .AddUserSecrets<Program>(optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        var appConfig = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        const string outputTemplate = "{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";

        var instrumentationKey = appConfig.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: outputTemplate);

        if (!context.HostingEnvironment.IsDevelopment())
        {
            logConfig = logConfig
                .WriteTo.ApplicationInsights(new TelemetryConfiguration(instrumentationKey), TelemetryConverter.Traces, LogEventLevel.Debug);
        }

        services.AddLogging(o => o.AddSerilog(logConfig.CreateLogger(), dispose: true));

        services.RegisterApplicationComponents(appConfig);
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
