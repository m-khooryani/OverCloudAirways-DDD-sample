using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OverCloudAirways.IdentityService.API;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables()
            .AddUserSecrets<Startup>();
    })
    .ConfigureFunctionsWorkerDefaults(workerApplicationBuilder =>
    {
        workerApplicationBuilder.UseMiddleware<LoggingMiddleware>();
        workerApplicationBuilder.UseMiddleware<AuthenticationMiddleware>();
    })
    .Build();

host.Run();
