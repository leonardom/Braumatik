using Braumatik.Domain;
using Braumatik.Domain.Entity;
using Shouldly;

namespace Braumatik.UnitTests.Domain.Entity;

public class FermentableTest
{
    [Theory]
    [InlineData("Light liquid malt extract", "Extract")]
    [InlineData("Light LME", "Extract")]
    [InlineData("Light dry malt extract", "Extract")]
    [InlineData("Light DME", "Extract")]
    [InlineData("Candi sugar", "Extract")]
    [InlineData("Extra pale lme", "Extract")]
    [InlineData("Foo (boil)", "Grain")]
    [InlineData("Caramel 30L", "Grain")]
    [InlineData("Crystal 60L", "Grain")]
    [InlineData("Caramunich", "Grain")]
    [InlineData("Special B", "Grain")]
    [InlineData("Some other item", "Grain")]
    public void Type_WhenCalled_ShouldReturnCorrectTypeForName(string name, string expectedType)
    {
        // Arrange
        var fermentable = Fermentable.Create(name, Mass.Kilograms(5.0));

        // Act
        var type = fermentable.Type();

        // Assert
        type.ShouldBe(expectedType);
    }

    [Theory]
    [InlineData("Light liquid malt extract", "Boil")]
    [InlineData("Light LME", "Boil")]
    [InlineData("Light dry malt extract", "Boil")]
    [InlineData("Light DME", "Boil")]
    [InlineData("Candi sugar", "Boil")]
    [InlineData("Extra pale lme", "Boil")]
    [InlineData("Foo (boil)", "Boil")]
    [InlineData("Caramel 30L", "Steep")]
    [InlineData("Crystal 60L", "Steep")]
    [InlineData("Caramunich", "Steep")]
    [InlineData("Special B", "Steep")]
    [InlineData("Some other item", "Mash")]
    [InlineData("Special B (mash)", "Mash")]
    [InlineData("Special B (boil)", "Boil")]
    public void Addition_WhenCalled_ShouldReturnCorrectAdditionForName(string name, string expectedAddition)
    {
        // Arrange
        var fermentable = Fermentable.Create(name, Mass.Kilograms(5.0));

        // Act
        var addition = fermentable.Addition();

        // Assert
        addition.ShouldBe(expectedAddition);
    }

    [Fact]
    public void GravityUnits_WhenCalled_ShouldCalculateCorrectGravityUnits()
    {
        // Arrange
        var fermentable = Fermentable.Create("Pale Malt", Mass.Kilograms(1.0));
        var volume = Volume.Gallons(1.0);

        // Act
        var gravityUnits = fermentable.GravityUnits(volume);

        // Assert
        gravityUnits.ToString().ShouldBe("76.4");
    }
}
