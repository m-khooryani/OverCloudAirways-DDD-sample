namespace OverCloudAirways.BuildingBlocks.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message)
        : base(message)
    {
    }
}
