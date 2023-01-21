namespace OverCloudAirways.BuildingBlocks.Domain.Exceptions;

public class TypedIdInitializationException : Exception
{
    public TypedIdInitializationException(string message)
        : base(message)
    {
    }
}
