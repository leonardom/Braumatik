namespace Braumatik.Domain.ValueObject;

public record Yield(double Value)
{
    public static Yield DefaultValue() => new(75.0);
    public static Yield ValueOrDefault(Yield? value) => value ?? DefaultValue();
}
