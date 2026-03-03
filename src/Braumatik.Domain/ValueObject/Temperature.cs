namespace Braumatik.Domain.ValueObject;

public class Temperature
{
    public double Value { get; set; }
    public TemperatureUnit Unit { get; set; }

    private Temperature(double value, TemperatureUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Temperature Farenheit(double value) => new(value, TemperatureUnit.Farenheit);
    public static Temperature Celsius(double value) => new(value, TemperatureUnit.Celsius);

    public Temperature ConvertTo(TemperatureUnit targetUnit)
    {
        if (Unit == targetUnit) return this;
        return (Unit, targetUnit) switch
        {
            (TemperatureUnit.Farenheit, TemperatureUnit.Celsius) => Celsius((Value - 32) * 5 / 9),
            (TemperatureUnit.Celsius, TemperatureUnit.Farenheit) => Farenheit((Value * 9 / 5) + 32),
            _ => throw new InvalidOperationException($"Unsupported conversion from {Unit} to {targetUnit}.")
        };
    }
}
