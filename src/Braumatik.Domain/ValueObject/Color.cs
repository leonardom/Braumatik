namespace Braumatik.Domain.ValueObject;

public class Color
{
    public double Value { get; private set; }
    public ColorUnit Unit { get; private set; }

    private Color(double value, ColorUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Color Create(double value, ColorUnit unit) => new(value, unit);
    public static Color SRM(double value) => Create(value, ColorUnit.SRM);
    public static Color EBC(double value) => Create(value, ColorUnit.EBC);
    public static Color Lovibond(double value) => Create(value, ColorUnit.Lovibond);

    public Color ConvertTo(ColorUnit targetUnit)
    {
        if (Unit == targetUnit) return this;
        return (Unit, targetUnit) switch
        {
            (ColorUnit.SRM, ColorUnit.EBC) => EBC(Value * 1.97),
            (ColorUnit.SRM, ColorUnit.Lovibond) => Lovibond((Value + 0.76) / 1.3546),
            (ColorUnit.EBC, ColorUnit.SRM) => SRM(Value * 0.508),
            (ColorUnit.EBC, ColorUnit.Lovibond) => Lovibond(((Value * 0.508) + 0.76) / 1.3546),
            (ColorUnit.Lovibond, ColorUnit.SRM) => SRM(1.3546 * Value - 0.76),
            (ColorUnit.Lovibond, ColorUnit.EBC) => EBC(((Value * 1.3546)-0.76) / 1.97),
            _ => throw new InvalidOperationException($"Unsupported conversion from {Unit} to {targetUnit}.")
        };
    }
}
