using Braumatik.Domain.Entity;
using Braumatik.Domain.ValueObject;
using Shouldly;

namespace Braumatik.UnitTests.Domain.Entity;

public class HopTest
{
    [Fact]
    public void Bitterness_ShouldCalculateCorrectly()
    {
        // Arrange
        var batchSize = Volume.Gallons(5.0);
        var earlyOg = 1.050;
        var weight = Mass.Ounces(1.0);
        var alphaAcids = 10.0;
        var time = Time.Minutes(60);
        var hop = Hop.Create("Cascade", weight, alphaAcids, HopUsage.Boil, HopForm.Pellet, time);

        // Act
        var bitterness = hop.Bitterness(earlyOg, batchSize);

        // Assert
        bitterness.ShouldBe(34.4);
    }

    [Fact]
    public void Bitterness_WhenDryHopUsage_ShouldCalculateToZero()
    {
        // Arrange
        var batchSize = Volume.Gallons(5.0);
        var earlyOg = 1.050;
        var weight = Mass.Ounces(1.0);
        var alphaAcids = 10.0;
        var time = Time.Minutes(60);
        var hop = Hop.Create("Cascade", weight, alphaAcids, HopUsage.DryHop, HopForm.Pellet, time);

        // Act
        var bitterness = hop.Bitterness(earlyOg, batchSize);

        // Assert
        bitterness.ShouldBe(0.0);
    }

    [Fact]
    public void Bitterness_WhenNoBoil_ShouldCalculateToZero()
    {
        // Arrange
        var batchSize = Volume.Gallons(5.0);
        var earlyOg = 1.050;
        var weight = Mass.Ounces(1.0);
        var alphaAcids = 10.0;
        var time = Time.Minutes(0);
        var hop = Hop.Create("Cascade", weight, alphaAcids, HopUsage.Boil, HopForm.Pellet, time);

        // Act
        var bitterness = hop.Bitterness(earlyOg, batchSize);

        // Assert
        bitterness.ShouldBe(0.0);
    }

    [Fact]
    public void Bitterness_WhenHopStandUsage_ShouldCalculateCorrectly()
    {
        // Arrange
        var batchSize = Volume.Liters(20.0);
        var earlyOg = 1.060;
        var weight = Mass.Grams(50.0);
        var alphaAcids = 10.0;
        var time = Time.Minutes(20);
        var hop = Hop.Create("Cascade", weight, alphaAcids, HopUsage.HopStand, HopForm.Pellet, time);

        // Act
        var bitterness = hop.Bitterness(earlyOg, batchSize);

        // Assert
        bitterness.ShouldBe(0.0);
    }
}
