using System.Reflection;

namespace DArch.Infrastructure.CleanArchitecture;

internal class AssemblyLayers
{
    public Assembly DomainLayer { get; init; }
    public Assembly ApplicationLayer { get; init; }
}
