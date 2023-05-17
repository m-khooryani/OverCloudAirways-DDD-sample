using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults(workerApplicationBuilder =>
    {
        workerApplicationBuilder.UseMiddleware<LoggingMiddleware>();
        workerApplicationBuilder.UseMiddleware<AuthenticationMiddleware>();
        workerApplicationBuilder.UseMiddleware<AuthorizationMiddleware>();
    })
    .Build();

host.Run();
