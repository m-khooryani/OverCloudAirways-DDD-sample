namespace DArch.Infrastructure.RetryPolicy;

public class PollyConfig
{
    public TimeSpan[] SleepDurations { get; init; }
}
