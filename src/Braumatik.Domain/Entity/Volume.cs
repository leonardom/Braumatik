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

    public Volume ToLiters()
    {
        return Unit switch
        {
            VolumeUnit.Milliliter => Create(Value / 1000, VolumeUnit.Liter),
            VolumeUnit.Gallon => Create(Value * 3.78541, VolumeUnit.Liter),
            _ => this,
        };
    }

    public Volume ToMilliliters()
    {
        return Unit switch
        {
            VolumeUnit.Liter => Create(Value * 1000, VolumeUnit.Milliliter),
            VolumeUnit.Gallon => Create(Value * 3.78541 * 1000, VolumeUnit.Milliliter),
            _ => this,
        };
    }

    public Volume ToGallons()
    {
        return Unit switch
        {
            VolumeUnit.Liter => Create(Value / 3.78541, VolumeUnit.Gallon),
            VolumeUnit.Milliliter => Create((Value / 1000) / 3.78541, VolumeUnit.Gallon),
            _ => this,
        };
    }

    public static Volume Create(double value, VolumeUnit unit) => new(value, unit);
    public static Volume Liters(double value) => Create(value, VolumeUnit.Liter);
    public static Volume Milliliters(double value) => Create(value, VolumeUnit.Milliliter);
    public static Volume Gallons(double value) => Create(value, VolumeUnit.Gallon);

    public override string ToString() => $"{Value:F2} {Unit}";
}
