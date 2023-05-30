using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Infrastructure.DomainServices.Users;

internal class GraphAPIUserRegistrationService : IGraphAPIUserRegistrationService
{
    private readonly GraphConfiguration _graphConfiguration;

    public GraphAPIUserRegistrationService(GraphConfiguration graphConfiguration)
    {
        _graphConfiguration = graphConfiguration;
    }

    public async Task RegisterAsync(Domain.Users.User user, CancellationToken cancellationToken)
    {
        var clientSecretCredential = new ClientSecretCredential(
            _graphConfiguration.TenantId, 
            _graphConfiguration.ClientId,
            _graphConfiguration.ClientSecret);
        var graphClient = new GraphServiceClient(clientSecretCredential);

        var graphUser = new Microsoft.Graph.Models.User
        {
            GivenName = user.GivenName,
            Surname = user.Surname,
            DisplayName = $"{user.GivenName} {user.Surname}",
            Identities = new List<ObjectIdentity>
            {
                new ObjectIdentity()
                {
                    SignInType = "emailAddress",
                    Issuer = "overcloudairwaysorg.onmicrosoft.com",
                    IssuerAssignedId = user.Email
                }
            },
            PasswordProfile = new PasswordProfile()
            {
                Password = "aZ123456",
                ForceChangePasswordNextSignIn = false
            },
            PasswordPolicies = "DisablePasswordExpiration",
            AdditionalData = new Dictionary<string, object>
            {
                { GetCompleteAttributeName("UserRole"), "Admin" }
            },
        };

        await graphClient.Users.PostAsync(graphUser, cancellationToken: cancellationToken);
    }

    private static string GetCompleteAttributeName(string attributeName)
    {
        var extensionAppClientId = "c81bc56e-4302-4d68-a84a-8ad333baa69e";
        var extensionClientId = extensionAppClientId.Replace("-", "");
        return $"extension_{extensionClientId}_{attributeName}";
    }
}
