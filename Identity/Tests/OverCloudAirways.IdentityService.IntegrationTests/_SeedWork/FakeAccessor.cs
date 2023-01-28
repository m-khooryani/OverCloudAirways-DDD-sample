using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.IdentityService.IntegrationTests._SeedWork;

class FakeAccessor : IUserAccessor
{
    private FakeAccessor()
    {
    }

    public static FakeAccessor Instance = new();

    private static Guid _userId;
    private static void ResetUserId()
    {
        _userId = Guid.NewGuid();
    }
    public Guid UserId => _userId;

    private static string _fullName;
    private static void ResetFullName()
    {
        _fullName = Guid.NewGuid().ToString();
    }
    public string FullName => _fullName;

    private Guid _storedUserId;
    private string _storedFullName;

    internal void Destroy()
    {
        _storedUserId = _userId;
        _storedFullName = _fullName;

        Reset();
    }

    internal void Rebuild()
    {
        _userId = _storedUserId;
        _fullName = _storedFullName;
    }

    internal static void Reset()
    {
        ResetUserId();
        ResetFullName();
    }
}
