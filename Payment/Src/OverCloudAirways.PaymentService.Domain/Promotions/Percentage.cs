using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace OverCloudAirways.PaymentService.Domain.Promotions;

public class Percentage : ValueObject
{
    public decimal Value { get; }

    private Percentage()
    {
    }

    private Percentage(decimal value)
    {
        Value = value;
    }

    public static Percentage Of(decimal value)
    {
        return new Percentage(value);
    }
}