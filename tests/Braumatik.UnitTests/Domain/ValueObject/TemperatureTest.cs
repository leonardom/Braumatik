using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.ValueObject;

public class TemperatureTest
{
    [Fact]
    public void Static_Constructors_SetValueAndUnit()
    {
        var f = Temperature.Farenheit(98.6);
        f.Value.ShouldBe(98.6);
        f.Unit.ShouldBe(TemperatureUnit.Farenheit);

        var c = Temperature.Celsius(37);
        c.Value.ShouldBe(37);
        c.Unit.ShouldBe(TemperatureUnit.Celsius);
    }

    [Theory]
    [InlineData(32.0, 0.0)]
    [InlineData(212.0, 100.0)]
    [InlineData(98.6, 37.0)]
    public void Farenheit_To_Celsius(double fValue, double expectedC)
    {
        var t = Temperature.Farenheit(fValue);

        var converted = t.ConvertTo(TemperatureUnit.Celsius);

        converted.Unit.ShouldBe(TemperatureUnit.Celsius);
        converted.Value.ShouldBe(expectedC, 1e-6);
    }

    [Theory]
    [InlineData(0.0, 32.0)]
    [InlineData(100.0, 212.0)]
    [InlineData(37.0, 98.6)]
    public void Celsius_To_Farenheit(double cValue, double expectedF)
    {
        var t = Temperature.Celsius(cValue);

        var converted = t.ConvertTo(TemperatureUnit.Farenheit);

        converted.Unit.ShouldBe(TemperatureUnit.Farenheit);
        converted.Value.ShouldBe(expectedF, 1e-6);
    }

    [Fact]
    public void ConvertTo_SameUnit_ReturnsSameInstance()
    {
        var original = Temperature.Celsius(20);

        var converted = original.ConvertTo(TemperatureUnit.Celsius);

        converted.ShouldBeSameAs(original);
    }
}
