namespace Braumatik.Domain.ValueObject;

public record Ppg(double Value)
{
    public const double Factor = 0.46214;

    public static Ppg FromYield(Yield yield) => new(yield.Value * Factor);
}
