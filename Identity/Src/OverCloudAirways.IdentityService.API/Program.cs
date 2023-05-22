using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Worker;
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
            .Enrich.FromLogContext();

        if (context.HostingEnvironment.IsDevelopment())
        {
            logConfig = logConfig.WriteTo.Console(outputTemplate: outputTemplate);
        }
        else
        {
            logConfig = logConfig
                .WriteTo.ApplicationInsights(new TelemetryConfiguration(instrumentationKey), TelemetryConverter.Traces, LogEventLevel.Debug);
        }

        services.AddLogging(o => o.AddSerilog(logConfig.CreateLogger(), dispose: true));

        services.RegisterApplicationComponents(appConfig);
    })
    .ConfigureFunctionsWorkerDefaults(workerApplicationBuilder =>
    {
        workerApplicationBuilder.UseMiddleware<StampMiddleware>();
        workerApplicationBuilder.UseWhen<LoggingMiddleware>((context) =>
        {
            return IsHttpTrigger(context);
        });
        workerApplicationBuilder.UseWhen<JsonResponseMiddleware>((context) =>
        {
            return IsHttpTrigger(context);
        });
        //workerApplicationBuilder.UseMiddleware<AuthenticationMiddleware>();
        //workerApplicationBuilder.UseMiddleware<AuthorizationMiddleware>();
    })
    .Build();

host.Run();

static bool IsHttpTrigger(FunctionContext context)
{
    return context.FunctionDefinition.InputBindings.Values.First(a => a.Type.EndsWith("Trigger")).Type == "httpTrigger";
}