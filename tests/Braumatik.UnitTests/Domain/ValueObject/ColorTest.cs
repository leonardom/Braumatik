using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.ValueObject;

public class ColorTest
{
    [Fact]
    public void Create_And_Static_Constructors_SetValueAndUnit()
    {
        var srmColor = Color.SRM(5.5);
        srmColor.Value.ShouldBe(5.5);
        srmColor.Unit.ShouldBe(ColorUnit.SRM);

        var ebcColor = Color.EBC(8.25);
        ebcColor.Value.ShouldBe(8.25);
        ebcColor.Unit.ShouldBe(ColorUnit.EBC);

        var lovibondColor = Color.Lovibond(3.14);
        lovibondColor.Value.ShouldBe(3.14);
        lovibondColor.Unit.ShouldBe(ColorUnit.Lovibond);
    }

    [Theory]
    [InlineData(10.0)]
    [InlineData(0.5)]
    [InlineData(20.25)]
    public void SRM_To_EBC_And_Lovibond_Conversions(double value)
    {
        var srm = Color.SRM(value);

        var ebc = srm.ConvertTo(ColorUnit.EBC);
        ebc.Unit.ShouldBe(ColorUnit.EBC);
        ebc.Value.ShouldBe(value * 1.97, 1e-6);

        var lov = srm.ConvertTo(ColorUnit.Lovibond);
        lov.Unit.ShouldBe(ColorUnit.Lovibond);
        lov.Value.ShouldBe((value + 0.76) / 1.3546, 1e-6);
    }

    [Theory]
    [InlineData(15.0)]
    [InlineData(1.0)]
    public void EBC_To_SRM_And_Lovibond_Conversions(double value)
    {
        var ebc = Color.EBC(value);

        var srm = ebc.ConvertTo(ColorUnit.SRM);
        srm.Unit.ShouldBe(ColorUnit.SRM);
        srm.Value.ShouldBe(value * 0.508, 1e-6);

        var lov = ebc.ConvertTo(ColorUnit.Lovibond);
        lov.Unit.ShouldBe(ColorUnit.Lovibond);
        lov.Value.ShouldBe(((value * 0.508) + 0.76) / 1.3546, 1e-6);
    }

    [Theory]
    [InlineData(5.0)]
    [InlineData(12.34)]
    public void Lovibond_To_SRM_And_EBC_Conversions(double value)
    {
        var lov = Color.Lovibond(value);

        var srm = lov.ConvertTo(ColorUnit.SRM);
        srm.Unit.ShouldBe(ColorUnit.SRM);
        srm.Value.ShouldBe(1.3546 * value - 0.76, 1e-6);

        var ebc = lov.ConvertTo(ColorUnit.EBC);
        ebc.Unit.ShouldBe(ColorUnit.EBC);
        ebc.Value.ShouldBe(((value * 1.3546) - 0.76) / 1.97, 1e-6);
    }

    [Fact]
    public void ConvertTo_SameUnit_ReturnsSameInstance()
    {
        // Arrange
        var original = Color.SRM(7.5);

        // Act
        var converted = original.ConvertTo(ColorUnit.SRM);

        // Assert
        converted.ShouldBeSameAs(original);
    }
}
