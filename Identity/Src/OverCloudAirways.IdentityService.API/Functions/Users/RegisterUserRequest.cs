using OverCloudAirways.IdentityService.Domain.Users;

namespace OverCloudAirways.IdentityService.API.Functions.Users;

public class RegisterUserRequest
{
    public UserType UserType { get; set; }
    public string Email { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
}