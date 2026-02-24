using Braumatik.Domain.ValueObject;

namespace Braumatik.Domain.Entity;

public class Mass
{
    public double Value { get; }
    public MassUnit Unit { get; }

    private static readonly Dictionary<MassUnit, double> ToGramsFactors = new()
    {
        { MassUnit.Gram, 1 },
        { MassUnit.Kilogram, 1000 },
        { MassUnit.Pound, 453.59237 },
        { MassUnit.Ounce, 28.3495231 }
    };

    private Mass(double value, MassUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Mass Create(double value, MassUnit unit) => new(value, unit);
    public static Mass Grams(double value) => new(value, MassUnit.Gram);
    public static Mass Kilograms(double value) => new(value, MassUnit.Kilogram);
    public static Mass Pounds(double value) => new(value, MassUnit.Pound);
    public static Mass Ounces(double value) => new(value, MassUnit.Ounce);

    public double ToGrams() => Value * ToGramsFactors[Unit];

    public Mass ConvertTo(MassUnit targetUnit)
    {
        double grams = ToGrams();
        double convertedValue = grams / ToGramsFactors[targetUnit];
        return Create(convertedValue, targetUnit);
    }

    public override string ToString() => $"{Value:F2} {Unit}";
}
