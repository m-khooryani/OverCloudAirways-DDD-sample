using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OverCloudAirways.IdentityService.API.GraphIntegration;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder
            .AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
