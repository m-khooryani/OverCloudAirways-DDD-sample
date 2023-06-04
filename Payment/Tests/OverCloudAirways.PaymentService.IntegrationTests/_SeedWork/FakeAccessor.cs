using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.PaymentService.IntegrationTests._SeedWork;

class FakeAccessor : IUserAccessor
{
    public FakeAccessor()
    {
    }

    private static Guid _userId;
    private static void ResetUserId()
    {
        _userId = Guid.NewGuid();
    }

    private static string _fullName;
    private static void ResetFullName()
    {
        _fullName = Guid.NewGuid().ToString();
    }

    Guid IUserAccessor.UserId { get => _userId; set => _userId = value; }
    string? IUserAccessor.TcpConnectionId { get => Guid.NewGuid().ToString(); set => throw new NotImplementedException(); }
    string IUserAccessor.FullName { get => _fullName; set => _fullName = value; }

    private Guid _storedUserId;
    private string _storedFullName;

    internal void SaveState()
    {
        _storedUserId = _userId;
        _storedFullName = _fullName;

        Reset();
    }

    internal void RestoreState()
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
