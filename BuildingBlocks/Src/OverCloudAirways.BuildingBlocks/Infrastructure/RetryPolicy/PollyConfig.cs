namespace OverCloudAirways.BuildingBlocks.Infrastructure.RetryPolicy;

public class PollyConfig
{
    public TimeSpan[] SleepDurations { get; init; }
}
