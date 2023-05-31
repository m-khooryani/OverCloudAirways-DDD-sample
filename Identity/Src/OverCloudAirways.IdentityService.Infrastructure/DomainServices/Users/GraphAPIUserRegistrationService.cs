using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.Infrastructure.DomainServices.Users;

internal class GraphAPIUserRegistrationService : IGraphAPIUserRegistrationService
{
    private readonly GraphConfiguration _graphConfiguration;
    private readonly ILogger _logger;

    public GraphAPIUserRegistrationService(
        GraphConfiguration graphConfiguration,
        ILogger logger)
    {
        _graphConfiguration = graphConfiguration;
        _logger = logger;
    }

    public async Task RegisterAsync(Domain.Users.User user, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting user registration process for user {UserId}", user.Id);

        var clientSecretCredential = new ClientSecretCredential(
            _graphConfiguration.TenantId,
            _graphConfiguration.ClientId,
            _graphConfiguration.ClientSecret);
        var graphClient = new GraphServiceClient(clientSecretCredential);
        var graphUser = CreateGraphUser(user);
        try
        {
            await graphClient.Users.PostAsync(graphUser, cancellationToken: cancellationToken);
            _logger.LogInformation("User {UserId} registration completed successfully.", user.Id);
        }
        catch (ODataError odataError)
        {
            _logger.LogError(odataError.Error.Code);
            _logger.LogError(odataError.Error.Message);
            throw;
        }
    }

    private Microsoft.Graph.Models.User CreateGraphUser(Domain.Users.User user)
    {
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
                    Issuer = _graphConfiguration.Issuer,
                    IssuerAssignedId = user.Email
                }
            },
            PasswordProfile = new PasswordProfile()
            {
                // static password for simplicity
                Password = "aZ123456",
                ForceChangePasswordNextSignIn = false
            },
            PasswordPolicies = "DisablePasswordExpiration",
            AdditionalData = new Dictionary<string, object>
            {
                { GetCompleteAttributeName("UserRole"), user.UserType.ToString() }
            },
        };
        return graphUser;
    }

    private string GetCompleteAttributeName(string attributeName)
    {
        var extensionAppClientId = _graphConfiguration.ExtensionAppClientId;
        return $"extension_{extensionAppClientId.Replace("-", "")}_{attributeName}";
    }
}
