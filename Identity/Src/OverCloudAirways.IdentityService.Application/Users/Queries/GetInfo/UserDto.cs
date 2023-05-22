using OverCloudAirways.IdentityService.Domain.Users;

public record UserDto(
    Guid Id, 
    UserType UserType,
    string Email,
    string GivenName,
    string Surname);
