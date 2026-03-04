using Braumatik.Domain.ValueObject;

namespace Braumatik.Domain.Entity; 

public class Volume
{
    public double Value { get; }
    public VolumeUnit Unit { get; }

    private Volume(double value, VolumeUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public Volume ToLiters() => ConvertTo(VolumeUnit.Liter);
    public Volume ToMilliliters() => ConvertTo(VolumeUnit.Milliliter);
    public Volume ToGallons() => ConvertTo(VolumeUnit.Gallon);

    public Volume ConvertTo(VolumeUnit targetUnit)
    {
        if (Unit == targetUnit) return this;
        return (Unit, targetUnit) switch
        {
            (VolumeUnit.Gallon, VolumeUnit.Liter) => Liters(Value * 3.78541),
            (VolumeUnit.Milliliter, VolumeUnit.Liter) => Liters(Value / 1000),
            (VolumeUnit.Liter, VolumeUnit.Milliliter) => Milliliters(Value * 1000),
            (VolumeUnit.Gallon, VolumeUnit.Milliliter) => Milliliters(Value * 3.78541 * 1000),
            (VolumeUnit.Liter, VolumeUnit.Gallon) => Gallons(Value / 3.78541),
            (VolumeUnit.Milliliter, VolumeUnit.Gallon) => Gallons((Value / 1000) / 3.78541),
            _ => throw new InvalidOperationException($"Unsupported conversion to {targetUnit}.")
        };
    }

    public static Volume Create(double value, VolumeUnit unit) => new(value, unit);
    public static Volume Liters(double value) => Create(value, VolumeUnit.Liter);
    public static Volume Milliliters(double value) => Create(value, VolumeUnit.Milliliter);
    public static Volume Gallons(double value) => Create(value, VolumeUnit.Gallon);

    public override string ToString() => $"{Value:F2} {Unit}";
}
