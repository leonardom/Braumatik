using Braumatik.Domain.Entity;
using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.Entity;

public class VolumeTest
{

    [Fact]
    public void Liters_WhenCalled_ReturnsVolumeInLiters()
    {
        // Arrange
        var value = 100.0;

        // Act
        var volume = Volume.Liters(value);

        // Assert
        volume.Value.ShouldBe(100);
        volume.Unit.ShouldBe(VolumeUnit.Liter);
    }

    [Fact]
    public void Milliliters_WhenCalled_ReturnsVolumeInMilliliters()
    {
        // Arrange
        var value = 250.0;

        // Act
        var volume = Volume.Milliliters(value);

        // Assert
        volume.Value.ShouldBe(250);
        volume.Unit.ShouldBe(VolumeUnit.Milliliter);
    }

    [Fact]
    public void Gallons_WhenCalled_ReturnsVolumeInGallons()
    {
        // Arrange
        var value = 2.0;
        
        // Act
        var volume = Volume.Gallons(value);
        
        // Assert
        volume.Value.ShouldBe(2);
        volume.Unit.ShouldBe(VolumeUnit.Gallon);
    }

    [Theory]
    [InlineData(1000, VolumeUnit.Milliliter, 1.0)]
    [InlineData(1, VolumeUnit.Gallon, 3.78541)]
    [InlineData(2.5, VolumeUnit.Liter, 2.5)]
    public void ToLiters_WhenCalled_ReturnsExpected(double value, VolumeUnit unit, double expectedLiters)
    {
        // Arrange
        var volume = Volume.Create(value, unit);

        // Act
        var converted = volume.ToLiters();

        // Assert
        converted.Unit.ShouldBe(VolumeUnit.Liter);
        converted.Value.ShouldBe(expectedLiters, 1e-6);
    }

    [Theory]
    [InlineData(1, VolumeUnit.Liter, 1000.0)]
    [InlineData(1, VolumeUnit.Gallon, 3785.41)]
    [InlineData(250, VolumeUnit.Milliliter, 250)]
    public void ToMilliliters_WhenCalled_ReturnsExpected(double value, VolumeUnit unit, double expectedMilliliters)
    {
        // Arrange
        var volume = Volume.Create(value, unit);

        // Act
        var converted = volume.ToMilliliters();

        // Assert
        converted.Unit.ShouldBe(VolumeUnit.Milliliter);
        converted.Value.ShouldBe(expectedMilliliters, 1e-6);
    }

    [Theory]
    [InlineData(3.78541, VolumeUnit.Liter, 1.0)]
    [InlineData(3785.41, VolumeUnit.Milliliter, 1.0)]
    [InlineData(2, VolumeUnit.Gallon, 2.0)]
    public void ToGallons_WhenCalled_ReturnsExpected(double value, VolumeUnit unit, double expectedGallons)
    {
        // Arrange
        var volume = Volume.Create(value, unit);

        // Act
        var converted = volume.ToGallons();

        // Assert
        converted.Unit.ShouldBe(VolumeUnit.Gallon);
        converted.Value.ShouldBe(expectedGallons, 1e-6);
    }

    [Theory]
    [InlineData(123.456, VolumeUnit.Liter)]
    [InlineData(123.456, VolumeUnit.Milliliter)]
    [InlineData(123.456, VolumeUnit.Gallon)]
    public void ToString_ReturnsValueAndUnit(double value, VolumeUnit unit)
    {
        // Arrange & Act
        var volume = Volume.Create(value, unit);

        volume.ToString().ShouldBe($"123.46 {unit}");
    }
}
