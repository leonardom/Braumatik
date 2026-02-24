using Braumatik.Domain.Entity;
using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.Entity;

public class MassTest
{

    [Fact]
    public void Grams_WhenCalled_ReturnsMassInGrams()
    {
        // Arrange
        var value = 100.0;

        // Act
        var mass = Mass.Grams(value);

        // Assert
        mass.Value.ShouldBe(100);
        mass.Unit.ShouldBe(MassUnit.Gram);
    }

    [Fact]
    public void Kilograms_WhenCalled_ReturnsMassInKilograms()
    {
        // Arrange
        var value = 100.0;

        // Act
        var mass = Mass.Kilograms(value);

        // Assert
        mass.Value.ShouldBe(100);
        mass.Unit.ShouldBe(MassUnit.Kilogram);
    }

    [Fact]
    public void Pounds_WhenCalled_ReturnsMassInPounds()
    {
        // Arrange
        var value = 100.0;

        // Act
        var mass = Mass.Pounds(value);

        // Assert
        mass.Value.ShouldBe(100);
        mass.Unit.ShouldBe(MassUnit.Pound);
    }

    [Fact]
    public void Ounces_WhenCalled_ReturnsMassInOunces()
    {
        // Arrange
        var value = 100.0;

        // Act
        var mass = Mass.Ounces(value);

        // Assert
        mass.Value.ShouldBe(100);
        mass.Unit.ShouldBe(MassUnit.Ounce);
    }

    [Theory]
    [InlineData(100, MassUnit.Gram, 100)]
    [InlineData(1.5, MassUnit.Kilogram, 1500)]
    [InlineData(2, MassUnit.Pound, 907.18474)]
    [InlineData(3.5, MassUnit.Ounce, 99.22333085)]
    public void ToGrams_WhenCalled_ReturnsExpected(double value, MassUnit unit, double expectedGrams)
    {
        // Arrange & Act
        var mass = Mass.Create(value, unit);

        // Assert
        mass.ToGrams().ShouldBe(expectedGrams, 1e-6);
    }

    [Theory]
    [InlineData(1000, MassUnit.Gram, MassUnit.Kilogram, 1)]
    [InlineData(1, MassUnit.Kilogram, MassUnit.Gram, 1000)]
    [InlineData(1, MassUnit.Pound, MassUnit.Gram, 453.59237)]
    [InlineData(16, MassUnit.Ounce, MassUnit.Pound, 1)]
    public void ConvertTo_WhenCalled_ReturnsExpected(double value, MassUnit from, MassUnit to, double expected)
    {
        // Arrange
        var mass = Mass.Create(value, from);

        // Act
        var converted = mass.ConvertTo(to);

        // Assert
        converted.Value.ShouldBe(expected, 1e-6);
        converted.Unit.ShouldBe(to);
    }

    [Theory]
    [InlineData(123.456, MassUnit.Gram)]
    [InlineData(123.456, MassUnit.Kilogram)]
    public void ToString_ReturnsFormattedValueAndUnit(double value, MassUnit unit)
    {
        // Arrange & Act
        var mass = Mass.Create(value, unit);

        // Assert
        mass.ToString().ShouldBe($"123.46 {unit}");
    }
}
