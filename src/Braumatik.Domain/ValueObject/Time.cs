namespace Braumatik.Domain.ValueObject;

public class Time
{
    public double Value { get; set; }
    public TimeUnit Unit { get; set; }

    private Time(double value, TimeUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Time Seconds(double value) => new(value, TimeUnit.Second);
    public static Time Minutes(double value) => new(value, TimeUnit.Minute);
    public static Time Hours(double value) => new(value, TimeUnit.Hour);

    public Time ConvertTo(TimeUnit targetUnit)
    {
        if (Unit == targetUnit) return this;
        return (Unit, targetUnit) switch
        {
            (TimeUnit.Second, TimeUnit.Minute) => Minutes(Value / 60),
            (TimeUnit.Second, TimeUnit.Hour) => Hours(Value / 3600),
            (TimeUnit.Minute, TimeUnit.Second) => Seconds(Value * 60),
            (TimeUnit.Minute, TimeUnit.Hour) => Hours(Value / 60),
            (TimeUnit.Hour, TimeUnit.Second) => Seconds(Value * 3600),
            (TimeUnit.Hour, TimeUnit.Minute) => Minutes(Value * 60),
            _ => throw new InvalidOperationException($"Unsupported conversion from {Unit} to {targetUnit}.")
        };
    }
}
