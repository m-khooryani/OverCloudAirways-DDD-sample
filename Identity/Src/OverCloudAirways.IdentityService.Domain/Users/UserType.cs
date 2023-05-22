using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.IdentityService.Domain.Users;

public abstract class UserType : Enumeration
{
    public static readonly UserType None = new NoneUserType();
    public static readonly UserType Customer = new CustomerUserType();
    public static readonly UserType Admin = new AdminUserType();

    private UserType(int value, string name) : base(value, name)
    {
    }

    private class NoneUserType : UserType
    {
        public NoneUserType() : base(0, "None")
        {
        }
    }

    private class CustomerUserType : UserType
    {
        public CustomerUserType() : base(1, "Customer")
        {
        }
    }

    private class AdminUserType : UserType
    {
        public AdminUserType() : base(2, "Admin")
        {
        }
    }
}