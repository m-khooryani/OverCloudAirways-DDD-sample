namespace OverCloudAirways.IdentityService.API.FunctionsMiddlewares;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizedAttribute : Attribute
{
    public IReadOnlyList<string> Roles { get; init; }

    public AuthorizedAttribute(string role, params string[] roles)
    {
        var list = new List<string>(roles)
        {
            role
        };
        Roles = list;
    }
}
