using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace OverCloudAirways.IdentityService.API.GraphIntegration
{
    public class CreateSubscriptionFunction
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public CreateSubscriptionFunction(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<CreateSubscriptionFunction>();
            _configuration = configuration;
        }

        [Function("CreateSubscriptionFunction")]
        public async Task Run([TimerTrigger("*/30 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            await CreateOrExtendSubscriptionAsync();
        }

        private async Task CreateOrExtendSubscriptionAsync()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = _configuration["TenantId"];
            var clientId = _configuration["ClientId"];
            var clientSecret = _configuration["ClientSecret"];
            var clientSecretCredential = new ClientSecretCredential(
                            tenantId, clientId, clientSecret);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var subscriptionsPage = await graphClient.Subscriptions
                .GetAsync();

            if (subscriptionsPage?.Value?.Any() ?? false)
            {
                _logger.LogInformation("Extending subscription");
                // Take the first subscription
                var firstSubscription = subscriptionsPage.Value.First();

                // Prepare update
                var subscriptionUpdate = new Subscription
                {
                    ExpirationDateTime = DateTimeOffset.UtcNow.AddDays(2)
                };

                // Update the subscription
                await graphClient.Subscriptions[firstSubscription.Id]
                    .PatchAsync(subscriptionUpdate);
            }
            else
            {
                _logger.LogInformation("Creating subscription");
                var requestBody = new Subscription
                {
                    ChangeType = "created,updated",
                    NotificationUrl = "https://overcloudairways-identity-graph-integration.azurewebsites.net/api/notifications",
                    Resource = "/users",
                    ExpirationDateTime = DateTimeOffset.UtcNow.AddDays(2),
                    ClientState = "SecretClientState",
                };
                await graphClient.Subscriptions.PostAsync(requestBody);
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
