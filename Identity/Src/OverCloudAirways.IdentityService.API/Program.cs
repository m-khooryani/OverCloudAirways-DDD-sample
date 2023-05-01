using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
    {
        configurationBuilder.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
