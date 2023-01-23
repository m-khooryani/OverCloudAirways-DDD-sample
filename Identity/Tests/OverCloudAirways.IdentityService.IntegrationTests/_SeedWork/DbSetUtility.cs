using Microsoft.EntityFrameworkCore;

namespace DArch.Samples.AppointmentService.IntegrationTests._SeedWork;

internal static class DbSetUtility
{
    public static void Clear<T>(this DbSet<T> set) where T : class
    {
        set.RemoveRange(set);
    }
}
