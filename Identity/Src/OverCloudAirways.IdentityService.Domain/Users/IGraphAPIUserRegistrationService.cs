namespace OverCloudAirways.IdentityService.Domain.Users;

public interface IGraphAPIUserRegistrationService
{
    Task RegisterAsync(User user, CancellationToken cancellationToken);
}
