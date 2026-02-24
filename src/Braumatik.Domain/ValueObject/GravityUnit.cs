namespace Braumatik.Domain.ValueObject;

public record GravityUnit(double Value)
{
    override public string ToString() => Value.ToString("F1");
}